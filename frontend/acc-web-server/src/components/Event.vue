<template>
  <v-container dense>
    <v-row dense>
      <v-col dense>
        <h2>Event</h2>
      </v-col>
    </v-row>
    <!-- Track -->
    <v-row dense>
      <v-col dense>
        <v-select
          dark
          dense
          v-model="event.track"
          :items="availableTracks"
          item-text="display"
          item-value="value"
          label="Track"
          persistent-hint
        ></v-select>
      </v-col>
    </v-row>
    <!-- Pre-race waiting time -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="event.preRaceWaitingTimeSeconds"
          label="Pre-race waiting time (seconds)"
          outlined
          shaped
          type="number"
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- post-race waiting time -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="event.postRaceSeconds"
          label="Post-race waiting time (seconds)"
          outlined
          shaped
          type="number"
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- Post-quali waiting time -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="event.postQualySeconds"
          label="Post-quali waiting time (seconds)"
          outlined
          shaped
          type="number"
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- Session-over waiting time -->
    <v-row dense>
      <v-col dense>
        <v-text-field
          dark
          prefix
          dense
          v-model="event.sessionOverTimeSeconds"
          label="Session-over waiting time (seconds)"
          outlined
          shaped
          type="number"
        ></v-text-field>
      </v-col>
    </v-row>
    <!-- Ambient temp -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Ambient Temp</v-subheader>
        <v-slider
          v-model="event.ambientTemp"
          :thumb-size="24"
          thumb-label="always"
          :max="40"
          :min="1"
        ></v-slider>
      </v-col>
    </v-row>
    <!-- Cloud level -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Cloud Level</v-subheader>
        <v-slider
          v-model="event.cloudLevel"
          :min="0"
          :max="1"
          :step="0.1"
          ticks="always"
          tick-size="4"
          :thumb-size="24"
          thumb-label="always"
        ></v-slider>
      </v-col>
    </v-row>
    <!-- Rain -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Rain</v-subheader>
        <v-slider
          dense
          v-model="event.rain"
          :min="0"
          :max="1"
          :step="0.1"
          ticks="always"
          tick-size="4"
          :thumb-size="24"
          thumb-label="always"
        ></v-slider>
      </v-col>
    </v-row>
    <!-- Weather randomness -->
    <v-row dense>
      <v-col dense>
        <v-subheader dark>Weather Randomness</v-subheader>
        <v-slider
          dense
          v-model="event.weatherRandomness"
          :thumb-size="24"
          thumb-label="always"
          :min="0"
          :max="7"
          icks="always"
          tick-size="4"
        ></v-slider>
      </v-col>
    </v-row>
    <!-- Is Fixed Condition Qualification -->
    <v-row dense>
      <v-col
        dense
        class="ma-2"
      >
        <v-switch
          dark
          v-model="event.isFixedConditionQualification"
          false-value=0
          label="Always Optimum"
        ></v-switch>
      </v-col>
    </v-row>
    <!-- Sessions -->
    <h3>Sessions: ({{ this.event.sessions.length }})</h3>
    <session
      v-for="session in event.sessions"
      v-bind:key="session.Id"
      v-bind:session="session"
      @delete-session="deleteSession"
      @insert-before="insertSessionBefore"
      @insert-after="insertSessionAfter"
    />
    <v-btn
      @click="$emit('event-save')"
      small
      color="green"
    >Save Event</v-btn>
  </v-container>
</template>

<script>
import Session from "./Session.vue";

export default {
  name: "Event",
  components: {
    Session
  },
  props: ["event"],
  data: () => ({
    trackMedals: ["0", "1", "2", "3"],
    cloudLevel: [
      "0.0",
      "0.1",
      "0.2",
      "0.3",
      "0.4",
      "0.5",
      "0.7",
      "0.8",
      "0.9",
      "1.0"
    ],
    availableTracks: [
      { value: "monza_2020", display: "Monza" },
      { value: "zolder_2020", display: "Zolder" },
      { value: "brands_hatch_2020", display: "Brands Hatch" },
      { value: "silverstone_2020", display: "Silverstone" },
      { value: "paul_ricard_2020", display: "Paul Ricard" },
      { value: "misano_2020", display: "Misano" },
      { value: "spa_2020", display: "Spa-Francorchamps" },
      { value: "nurburgring_2020", display: "Nurburgring" },
      { value: "barcelona_2020", display: "Barcelona Catalunya" },
      { value: "hungaroring_2020", display: "Hungaroring" },
      { value: "zandvoort_2020", display: "Zandvoort" },
      { value: "kyalami_2019", display: "Kyalami" },
      { value: "mount_panorama_2019", display: "Bathurst Mount Panorama" },
      { value: "suzuka_2019", display: "Suzuka" },
      { value: "laguna_seca_2019", display: "Laguna Seca" },
      { value: "imola_2020", display: "Imola" }
    ]
  }),
  methods: {
    reflowSessions() {
      for (let i = 0; i < this.event.sessions.length; i++) {
        this.event.sessions[i].Id = i;
        console.log(JSON.stringify(this.event.sessions[i]));
      }
    },
    insertSessionBefore(id) {
      let newSessions = [];

      for (let i = 0; i < this.event.sessions.length; i++) {
        if (this.event.sessions[i].Id == id) {
          let newSession = {
            hourOfDay: 6,
            dayOfWeekend: 2,
            timeMultiplier: 1,
            sessionType: "P",
            sessionDurationMinutes: 10,
            Id: this.event.sessions.length
          };

          newSessions.push(newSession);
        }
        newSessions.push(this.event.sessions[i]);
      }

      this.event.sessions = newSessions;

      this.reflowSessions();
    },
    insertSessionAfter(id) {
      let newSessions = [];

      for (let i = 0; i < this.event.sessions.length; i++) {
        newSessions.push(this.event.sessions[i]);

        if (this.event.sessions[i].Id == id) {
          let newSession = {
            hourOfDay: 6,
            dayOfWeekend: 2,
            timeMultiplier: 1,
            sessionType: "P",
            sessionDurationMinutes: 10,
            Id: this.event.sessions.length
          };

          newSessions.push(newSession);
        }
      }

      this.event.sessions = newSessions;

      this.reflowSessions();
    },
    deleteSession(id) {
      //Dont delete the last session
      if (this.event.sessions.length == 1) {
        alert("Must have at least 1 session");
        return;
      }

      for (let i = 0; i < this.event.sessions.length; i++) {
        if (this.event.sessions[i].Id == id) {
          this.event.sessions.splice(i, 1);
          continue;
        }
      }
    }
  }
};
</script>

<style scoped>
</style>