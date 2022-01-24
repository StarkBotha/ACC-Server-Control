<template>
  <v-app>
    <v-app-bar
      app
      class="black white--text"
    >
      <div class="d-flex align-center">
        <img
          src="./assets/retrologo.png"
          style="margin-right: 10px;"
        />
        RETRO E-SPORTS - ACC Dedicated Server manager
      </div>

      <v-spacer></v-spacer>
    </v-app-bar>

    <v-content class="blue-grey darken-3 white--text">
      <server-status
        v-bind:serverStatus="serverStatus"
        v-on:server-start="startServer"
        v-on:server-stop="stopServer"
        v-on:server-refresh="getServerStatus"
      />
      <v-tabs
        dark
        slider-color="red accent-4"
      >
        <v-tab ripple>Settings</v-tab>
        <v-tab ripple>Event and Sessions</v-tab>
        <v-tab ripple>Event Rules</v-tab>
        <v-tab-item class="blue-grey darken-3 white--text">
          <Settings
            v-bind:settings="settings"
            v-on:settings-save="saveSettings"
          />
        </v-tab-item>
        <v-tab-item class="blue-grey darken-3 white--text">
          <event
            v-bind:event="event"
            v-on:event-save="saveEvent"
          />
        </v-tab-item>
        <v-tab-item class="blue-grey darken-3 white--text">
          <event-rules
            v-bind:eventrules="eventRules"
            v-on:event-rules-save="saveEventRules"
          />
        </v-tab-item>
      </v-tabs>
    </v-content>
  </v-app>
</template>

<script>
import ServerStatus from "./components/ServerStatus.vue";
import Settings from "./components/Settings.vue";
import Event from "./components/Event.vue";
import EventRules from "./components/EventRules.vue";

import axios from "axios";

export default {
  name: "App",
  components: {
    ServerStatus,
    Settings,
    Event,
    EventRules
  },
  data() {
    return {
      serverStatus: "",
      settings: {},
      event: {},
      eventRules: {},
      baseUrl: process.env.VUE_APP_APIURL
    };
  },
  methods: {
    startServer() {
      axios
        .get(this.baseUrl + "Start")
        .then(() => {
          this.getServerStatus();
        })
        .catch(err => console.log(err));
    },
    stopServer() {
      axios
        .get(this.baseUrl + "Stop")
        .then(() => {
          this.getServerStatus();
        })
        .catch(err => console.log(err));
    },
    getServerStatus() {
      axios
        .get(this.baseUrl + "GetServerStatus")
        .then(res => (this.serverStatus = res.data))
        .catch(err => console.log(err));
    },
    getSettings() {
      axios
        .get(this.baseUrl + "GetSettings")
        .then(res => (this.settings = res.data))
        .catch(err => console.log(err));
    },
    getEvent() {
      axios
        .get(this.baseUrl + "GetEvent")
        //.then(res => (this.event = res.data))
        .then(response => {
          this.event = response.data;
          for (let i = 0; i < this.event.sessions.length; i++) {
            this.event.sessions[i].Id = i;
            console.log(JSON.stringify(this.event.sessions[i]));
          }
          //console.log(JSON.stringify(this.event));
        })
        .catch(err => console.log(err));
    },
    getEventRules() {
      axios
        .get(this.baseUrl + "GetEventRules")
        //.then(res => (this.eventRules = res.data))
        .then(response => {
          this.eventRules = response.data;
        })
        .catch(err => console.log(err));
    },
    saveSettings() {
      fetch(this.baseUrl + "SaveSettings", {
        method: "post",
        body: JSON.stringify(this.settings),
        headers: {
          "Content-Type": "application/json"
        }
      })
        .then(function(response) {
          if (!response.ok) alert(response.statusText);
          else alert("Settings saved");
        })
        .catch(error => {
          console.log(error);
        });
    },
    saveEvent() {
      if (this.event.isFixedConditionQualification === false)
        this.event.isFixedConditionQualification = 0;
      if (this.event.isFixedConditionQualification === true)
        this.event.isFixedConditionQualification = 1;
      fetch(this.baseUrl + "SaveEvent", {
        method: "post",
        body: JSON.stringify(this.event),
        headers: {
          "Content-Type": "application/json"
        }
      })
        .then(function(response) {
          if (!response.ok) alert(response.statusText);
          else alert("Event details saved");
        })
        .catch(error => {
          console.log(error);
        });
    },
    saveEventRules() {
      fetch(this.baseUrl + "SaveEventRules", {
        method: "post",
        body: JSON.stringify(this.eventRules),
        headers: {
          "Content-Type": "application/json"
        }
      })
        .then(function(response) {
          if (!response.ok) alert(response.statusText);
          else alert("Event details saved");
        })
        .catch(error => {
          console.log(error);
        });
    }
  },
  created() {
    //axios.get("static/config.json").then(response => {this.baseUrl = response.data.baseURL;});
    this.getServerStatus();
    this.getSettings();
    this.getEvent();
    this.getEventRules();
  }
};
</script>

<style scoped>
</style>