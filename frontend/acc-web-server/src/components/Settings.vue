<template>
  <v-container dense>
    <v-row dense>
      <v-col>
        <h2>Settings</h2>
      </v-col>
    </v-row>
    <!-- Server Name -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="settings.serverName"
          label="Server Name"
          outlined
          shaped
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- Admin password -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="settings.adminPassword"
          label="Admin Pwd"
          outlined
          shaped
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- Car Group -->
    <v-row dense>
      <v-col dense>
        <v-select
          dark
          dense
          v-model="settings.carGroup"
          :items="carGroups"
          item-text="display"
          item-value="value"
          label="Car Group"
          persistent-hint
        ></v-select>
      </v-col>
    </v-row>
    <!-- Track Medals -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Track Medals</v-subheader>
        <v-slider
          dark
          prefix
          dense
          v-model="settings.trackMedalsRequirement"
          class="align-center"
          :max="3"
          :min="0"
          :tick-labels="trackMedals"
          ticks="always"
          tick-size="4"
        >
        </v-slider>
      </v-col>
    </v-row>
    <!-- Safety Rating -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Safety Rating</v-subheader>
        <v-slider
          v-model="settings.safetyRatingRequirement"
          :thumb-size="24"
          thumb-label="always"
          :max="99"
          :min="0"
        ></v-slider>
      </v-col>
    </v-row>
    <!-- Racecraft -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Racecraft</v-subheader>
        <v-slider
          v-model="settings.racecraftRatingRequirement"
          :thumb-size="24"
          thumb-label="always"
          :max="99"
          :min="0"
        ></v-slider>
      </v-col>
    </v-row>
    <!-- Driver Password -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="settings.password"
          label="Driver Password"
          outlined
          shaped
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- Spectator Password -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="settings.spectatorPassword"
          label="Spectator Password"
          outlined
          shaped
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- Max Car slots -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Max car slots</v-subheader>
        <v-slider
          v-model="settings.maxCarSlots"
          :thumb-size="24"
          thumb-label="always"
          :max="60"
          :min="0"
        ></v-slider>
      </v-col>
    </v-row>
    <!-- Dump Leaderboard -->
    <v-row dense>
      <v-col dense>
        <v-switch
          dark
          v-model="settings.dumpLeaderboards"
          label="Dump Leaderboard"
        ></v-switch>
      </v-col>
    </v-row>
    <!-- Race Locked -->
    <v-row dense>
      <v-col dense>
        <v-switch
          dark
          v-model="settings.isRaceLocked"
          label="Race Locked"
        ></v-switch>
      </v-col>
    </v-row>
    <!-- Randomize -->
    <v-row dense>
      <v-col dense>
        <v-switch
          dark
          v-model="settings.randomizeTrackWhenEmpty"
          label="Randomize when empty"
        ></v-switch>
      </v-col>
    </v-row>
    <!-- Auto DQ -->
    <v-row dense>
      <v-col dense>
        <v-switch
          dark
          v-model="settings.allowAutoDQ"
          label="Auto DQ"
        ></v-switch>
      </v-col>
    </v-row>
    <!-- Short Formation -->
    <v-row dense>
      <v-col dense>
        <v-switch
          dark
          v-model="settings.shortFormationLap"
          label="Short Formation"
        ></v-switch>
      </v-col>
    </v-row>
    <!-- Formation Lap -->
    <v-row dense>
      <v-col dense>
        <v-select
          dark
          dense
          v-model="settings.formationLapType"
          :items="formationTypes"
          item-text="display"
          item-value="value"
          label="Formation Type"
          persistent-hint
        ></v-select>
      </v-col>
    </v-row>
    <v-btn
      @click="$emit('settings-save')"
      small
      color="green"
    >Save Settings</v-btn>
  </v-container>
</template>

<script>
export default {
  name: "Settings",
  props: ["settings"],
  data: () => ({
    trackMedals: ["0", "1", "2", "3"],
    formationTypes: [
      { value: 0, display: "Manual" },
      { value: 1, display: "Old Limiter" },
      { value: 2, display: "New Limiter" },
    ],
    carGroups: [
      { value: "FreeForAll", display: "Mixed" },
      { value: "GT3", display: "GT3" },
      { value: "GT4", display: "GT4" },
      { value: "Cup", display: "Cup" },
      { value: "ST", display: "Trofeo" },
    ],
  }),
};
</script>

<style scoped>
</style>