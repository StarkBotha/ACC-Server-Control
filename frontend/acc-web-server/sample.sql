USE [Axis_Subsc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[Transactions_UpdateTrigger] ON [dbo].[Transactions]
AFTER UPDATE
AS 
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;


    --Manual check for Id columns to link back to the source subcription DB and update START:
    DECLARE @TransactionStatus VARCHAR(50);
    DECLARE @PaymentChannelBankAccountNumber nvarchar(20);
    DECLARE @PaymentChannelAccountType nvarchar(20);
    DECLARE @PaymentChannelPaymentServiceType VARCHAR(100);
    DECLARE @ChosenPaymentServiceType VARCHAR(100);
    DECLARE @MerchantBranchProductNumber VARCHAR(50);


    select @PaymentChannelBankAccountNumber = bap.AccountNumber, @PaymentChannelAccountType = bap.AccountType, @PaymentChannelPaymentServiceType = ps.Description
    from inserted i inner join Axis_Subsc..PaymentChannels pc on i.ExecutedOnPaymentChannelId = pc.Id
        inner join Axis_Subsc..BankAccountPaymentDetails bap on pc.BankAccountDetailId = bap.Id
        inner join Axis_Subsc..PaymentService ps on pc.PaymentServiceId = ps.Id

    select @TransactionStatus = txs.TransactionState
    from inserted i inner join Axis_Subsc..TransactionStatuses txs on i.TransactionStatusId = txs.Id

    select @ChosenPaymentServiceType = ps.Description
    from inserted i inner join PaymentService ps on i.ChosenPaymentServiceId = ps.Id

    select @MerchantBranchProductNumber = mbp.MerchantBranchProductNumber
    from inserted i inner join MerchantBranchProducts mbp on i.MerchantBranchProductId = mbp.Id;
    --Manual check for Id columns to link back to the source subcription DB and update END:

    DROP TABLE IF EXISTS #TriggerInserter;
    select *
    into #TriggerInserter
    from inserted

    CREATE NONCLUSTERED INDEX ix_TemptyAxisTx ON #TriggerInserter (Id);
    -- Insert statements for trigger here
    --Using COLUMNS_UPDATED get the list of columns and join to sysobjects to get the actual name of the columns and populate 
    --the @Columns_Updated variable
    DECLARE @sql_executer VARCHAR(MAX);
    --Declare variables section END

    SET @sql_executer = 'Update ReportDB..TransactionReportData set ';

    DECLARE @idTable INT;

    SELECT @idTable = T.id
    FROM sysobjects P
        JOIN sysobjects T ON P.parent_obj = T.id
    WHERE P.id = @@procid;

    DECLARE @Columns_Updated VARCHAR(5000);

    SELECT @Columns_Updated = ISNULL(@Columns_Updated + ', ', '') + name + '= i.' + name
    FROM syscolumns
    WHERE id = @idTable
        AND name in 
        (
            'AcknowledgementFlag',
            'ChosenPaymentServiceId',
            'Description',
            'ErrorSourceSystem',
            'ExecutedOnPaymentChannelId',
            'ExecutedOnPaymentMethodId',
            'IsRetry',
            'MerchantBranchProductId',
            'MerchantReference',
            'OnceOffPaymentsId',
            'OutcomeCode',
            'OutcomeDescription',
            'PaymentRequestId',
            'TotalCostInCents',
            'TransactionStatusId',
            'WhenInitiated',
            'WhenUpdated'
        )
        AND CONVERT(VARBINARY, REVERSE(COLUMNS_UPDATED()))&POWER(CONVERT(BIGINT, 2), colorder - 1) > 0;


    INSERT INTO TriggerLogger
        (Logger)
    VALUES('New columns transaction Update');
    INSERT INTO TriggerLogger
        (Logger)
    VALUES(ISNULL(@Columns_Updated, '@Columns_Updated transaction was null!'));

    set @sql_executer  = @sql_executer + @Columns_Updated;
    INSERT INTO TriggerLogger
        (Logger)
    VALUES('New combined transaction update query');

    INSERT INTO TriggerLogger
        (Logger)
    VALUES(ISNULL(@sql_executer,'@sql_executer combined transaction query was null!'));


    --Add Id column lookup data to the update string. e.g. PaymentArrangementStatusId changes:
    SET @sql_executer = @sql_executer + ' ,TransactionStatus =''' + ISNULL(@TransactionStatus,'') + ''', PaymentChannelBankAccountNumber = ''' + ISNULL(@PaymentChannelBankAccountNumber,'') +'''
		 , PaymentChannelAccountType = ''' + ISNULL(@PaymentChannelAccountType,'') +''' , PaymentChannelPaymentServiceType = ''' + ISNULL(@PaymentChannelPaymentServiceType,'') 
		 +''', ChosenPaymentServiceType = ''' + ISNULL(@ChosenPaymentServiceType,'')  +'''
		, MerchantBranchProductNumber = ''' + ISNULL(@MerchantBranchProductNumber, '') + ''''




    --Join to the 'insert' table which contains the updates
    SET @sql_executer = @sql_executer + ' from  ReportDB..TransactionReportData ird
											 inner join #TriggerInserter i on ird.TransactionId=i.Id where ird.IsHistoryRecord = 0 ';
    --Keep a log of the update statements run. Can take this out when we've had the replication running for a while

    INSERT INTO TriggerLogger
        (Logger)
    VALUES('Final Transaction update Query');
    INSERT INTO TriggerLogger
        (Logger)
    VALUES(ISNULL(@sql_executer, 'Final Transaction update Query is null!'));



    if(@sql_executer IS NULL)
		 begin
        set @sql_executer='NULL in updater for transaction'
    end
    INSERT INTO TriggerLogger
        (Logger)
    VALUES(@sql_executer);

    BEGIN TRY
	exec(@sql_executer);
	END TRY
BEGIN CATCH
    IF XACT_STATE() = -1 ROLLBACK
	insert into TriggerLogger
        (Logger)
    SELECT 'Transaction Update Error! : ' + ERROR_MESSAGE()  
	
END CATCH

END
GO

ALTER TABLE [dbo].[Transactions] ENABLE TRIGGER [Transactions_UpdateTrigger]
GO


