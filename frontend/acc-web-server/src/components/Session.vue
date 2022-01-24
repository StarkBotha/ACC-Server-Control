<template>
  <v-container>
    <!-- <div>ID: {{session.Id}}</div> -->

    <v-card :elevation="4" class="pa-md-4 mx-lg-auto">
      <v-row dense>
        <v-col dense class="text-right">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-btn class="mx-2 .float-right" fab dark small right color="primary" v-on="on" @click="$emit('insert-before',session.Id)">
                <v-icon dark>mdi-plus</v-icon>
              </v-btn>
            </template>
            <span>Add Session Before</span>
          </v-tooltip>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col dense>
          <v-select
            dense
            v-model="session.sessionType"
            :items="sessionType"
            item-text="display"
            item-value="value"
            label="Session Type"
            persistent-hint
          ></v-select>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col dense>
          <v-subheader>Session Hour</v-subheader>
          <v-slider
            dense
            v-model="session.hourOfDay"
            :min="0"
            :max="23"
            ticks="always"
            tick-size="4"
            :thumb-size="24"
            thumb-label="always"
          ></v-slider>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col dense>
          <v-subheader>Session Day</v-subheader>
          <v-slider
            dense
            v-model="session.dayOfWeekend"
            :min="1"
            :max="3"
            ticks="always"
            tick-size="4"
            :thumb-size="24"
            thumb-label="always"
          ></v-slider>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col dense>
          <v-subheader>Time Multiplier</v-subheader>
          <v-slider
            dense
            v-model="session.timeMultiplier"
            :min="0"
            :max="24"
            ticks="always"
            tick-size="4"
            :thumb-size="24"
            thumb-label="always"
          ></v-slider>
        </v-col>
      </v-row>
      <v-row dense>
        <v-col dense>
          <v-subheader>Session Duration (minutes)</v-subheader>
          <v-slider
            dense
            v-model="session.sessionDurationMinutes"
            :min="5"
            :max="1440"
            :step="5"
            ticks="always"
            tick-size="4"
            :thumb-size="24"
            thumb-label="always"
          ></v-slider>
        </v-col>
      </v-row>

      <v-row dense>
        <v-col dense class="text-right">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-btn
                class="mx-2 .float-right"
                fab
                dark
                small
                right
                color="red"
                v-on="on"
                @click="$emit('delete-session',session.Id)"
              >
                <v-icon dark>mdi-minus</v-icon>
              </v-btn>
            </template>
            <span>Remove session</span>
          </v-tooltip>

          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-btn class="mx-2 .float-right" fab dark small right color="primary" v-on="on" @click="$emit('insert-after',session.Id)">
                <v-icon dark>mdi-plus</v-icon>
              </v-btn>
            </template>
            <span>Add Session After</span>
          </v-tooltip>
        </v-col>
      </v-row>      
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: "Session",
  props: ["session"],
  data: () => ({
    sessionType: [
      { value: "P", display: "Practice" },
      { value: "Q", display: "Qualifying" },
      { value: "R", display: "Race" }
    ]
  }),
  methods: {
    removeSession(id) {
      alert("remove " + id);
    }
  }
};
</script>

<style scoped>
.eventSession {
  border: 1px dashed black;
}
</style>