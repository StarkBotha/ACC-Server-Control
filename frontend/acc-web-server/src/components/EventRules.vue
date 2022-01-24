<template>
  <v-container dense>
    <v-row dense>
      <v-col dense>
        <h2>Event Rules</h2>
      </v-col>
    </v-row>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Qualify Standing Type</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >Fastest lap or average lap (running Endurance mode for multiple Q sessions) . Use Fastest Lap, averaging Qualy is not yet officially supported.</v-card-text>

      <!-- Qualifying Standing Type -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-select
            dark
            dense
            v-model="eventrules.qualifyStandingType"
            :items="availableQualifyingStandingType"
            item-text="display"
            item-value="value"
            label="Qualify Standing Type"
            persistent-hint
          ></v-select>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Pit Window Length</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >Defines a pit window at the middle of the race. Obviously covers the Sprint series format. -1 will disable the pit window. Use this combined with a mandatoryPitstopCount = 1.</v-card-text>
      <!-- Pit Window Length -->
      <v-row dense>
        <v-col dense dark class="ma-2">
          <v-subheader dark>Pit Window Length (in seconds)</v-subheader>
          <v-slider
            v-model="eventrules.pitWindowLength"
            :thumb-size="24"
            thumb-label="always"
            :max="3600"
            :min="-1"
          ></v-slider>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Driver Stint time</span>
      </v-card-title>

      <v-card-text class="title font-weight-light white--text">
        Defines the maximum time a driver can stay out without getting a penalty. Can be used to balance fuel efficient cars in endurance races. The stint time resets in the pitlane, no real stop is required.
        -1 will disable the stint times.
      </v-card-text>
      <!-- Driver Stint time -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-subheader dark>Driver Stint Time (in seconds)</v-subheader>
          <v-slider
            v-model="eventrules.driverStintTimeSec"
            :thumb-size="24"
            thumb-label="always"
            :max="3600"
            :min="-1"
          ></v-slider>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Mandatory Pitstop count</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >Defines the basic mandatory pit stops. If the value is greater zero, any car that did not execute the mandatory pitstops will be disqualified at the end of the race. A value of zero disables the feature</v-card-text>
      <!-- Mandatory Pitstop count -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-subheader dark>Mandatory Pitstop count</v-subheader>
          <v-slider
            v-model="eventrules.mandatoryPitstopCount"
            :thumb-size="24"
            thumb-label="always"
            :max="10"
            :min="0"
          ></v-slider>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Max total driving time</span>
      </v-card-title>

      <v-card-text class="title font-weight-light white--text">
        Restricts the maximum driving time for a single driver. Is only useful for driver swap situations and allows to enforce a minimum driving time for each driver (IRL this is used to make sure mixed teams like Pro/Am have a fair distribution of the slower drivers). -1 disables the feature.
        Will set the maximum driving time for the team size defined by “maxDriversCount”, always make sure both are set.
      </v-card-text>
      <!-- Max total driving time -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-subheader dark>Max total driving time</v-subheader>
          <v-slider
            v-model="eventrules.maxTotalDrivingTime"
            :thumb-size="24"
            thumb-label="always"
            :max="240"
            :min="-1"
          ></v-slider>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Max Drivers Count</span>
      </v-card-title>

      <v-card-text class="title font-weight-light white--text">
        In driver swap situations, set this to the maximum number of drivers on a car. When an entry has fewer drivers than maxDriversCount, maxTotalDrivingTime is automatically compensated so that those "smaller" entries are also able to complete the race
        Example: 3H race length, 65 minutes driverStintTimeSec and 65 minutes maxTotalDrivingTime will result in 65 minutes of maxTotalDrivingTime for entries of 3 and 105 (!) minutes for entries of 2.
      </v-card-text>
      <!-- Max Drivers Count -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-subheader dark>Max Drivers Count</v-subheader>
          <v-slider
            v-model="eventrules.maxDriversCount"
            :thumb-size="24"
            thumb-label="always"
            :max="6"
            :min="1"
          ></v-slider>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Is Refueling allowed in race</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >Defines if refuelling is allowed during the race pitstops.</v-card-text>
      <!-- Is Refueling allowed in race -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-switch
            dark
            v-model="eventrules.isRefuellingAllowedInRace"
            label="Refueling allowed in race"
          ></v-switch>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Is Refueling time fixed</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >If enabled, any refuelling will take the same amount of time. If turned off, refuelling will consume time linear to the amount refuelled. Very useful setting to balance fuel efficient cars, especially if combined with other features. </v-card-text>
      <!-- Is Refueling time fixed -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-switch
            dark
            v-model="eventrules.isRefuellingTimeFixed"
            label="Refueling time fixed"
          ></v-switch>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Is Mandatory Pitstop Refuelling Required</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >Defines if a mandatory pitstop requires refuelling. </v-card-text>
      <!-- Is Refueling time fixed -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-switch
            dark
            v-model="eventrules.isMandatoryPitstopRefuellingRequired"
            label="Mandatory Pitstop Refuelling Required"
          ></v-switch>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Is Mandatory Pitstop Tyre Change Required</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >Defines if a mandatory pitstop requires a tyre change. </v-card-text>
      <!-- Is Refueling time fixed -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-switch
            dark
            v-model="eventrules.isMandatoryPitstopTyreChangeRequired "
            label="Mandatory Pitstop Tyre Change Required"
          ></v-switch>
        </v-col>
      </v-row>
    </v-card>

    <v-card class="blue-grey darken-3 white--text">
      <v-card-title>
        <v-icon large left class="white--text">mdi-alert-circle-outline</v-icon>
        <span class="headline font-weight-bold">Is Mandatory Pitstop Driver Swap Required</span>
      </v-card-title>

      <v-card-text
        class="title font-weight-light white--text"
      >Defines if a mandatory pitstop requires a driver swap. Will only be effective for cars in driver swap situations; even in a mixed field this will be skipped for cars with a team size of 1 driver. </v-card-text>
      <!-- Is Refueling time fixed -->
      <v-row dense>
        <v-col dense class="ma-2">
          <v-switch
            dark
            v-model="eventrules.isMandatoryPitstopSwapDriverRequired"
            label="Mandatory Pitstop Driver Swap Required"
          ></v-switch>
        </v-col>
      </v-row>
    </v-card>

    <v-btn @click="$emit('event-rules-save')" small color="green">Save Rules</v-btn>
  </v-container>
</template>

<script>
export default {
  name: "EventRules",
  props: ["eventrules"],
  data: () => ({
    availableQualifyingStandingType: [{ value: 1, display: "Fastest Lap" }]
  })
};
</script>

<style scoped>
</style>