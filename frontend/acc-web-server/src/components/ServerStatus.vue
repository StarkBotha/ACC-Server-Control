<template>
  <v-card class="mx-auto" outlined>
    <v-list-item three-line>
      <v-list-item-content>
        <div class="headline mb-1">Server Status:</div>
        <span
          v-bind:class="{'serverOnline':serverStatus==='Online','serverOffline':serverStatus==='Offline'}"
        >{{serverStatus}}</span>
      </v-list-item-content>
    </v-list-item>
    <v-card-actions>
      <v-btn class="green white--text" @click="$emit('server-start')">Start</v-btn>
      <v-btn @click="$emit('server-stop')">Stop</v-btn>
      <v-btn @click="$emit('server-refresh')">Refresh</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: "ServerStatus",
  props: ["serverStatus"]
};
</script>

<style scoped>
.serverOnline {
  background: green;
  color: white;
}
.serverOffline {
  background: red;
  color: white;
}
* {
  padding: 15px;
}
</style>