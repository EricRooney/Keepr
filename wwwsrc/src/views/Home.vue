<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col">
        <h1>Welcome Home</h1>
      </div>
    </div>
    <KeepCard />
  </div>
</template>

<script>
import KeepCard from "@/components/KeepCard.vue";
export default {
  name: "home",
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  components: {
    KeepCard
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  }
};
</script>
