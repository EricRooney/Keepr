<template>
  <div class="row">
    <div class="col-3" v-for="(publicKeep) in publicKeeps" :key="publicKeep.id">
      <div class="card">
        <div
          :style="{ 'background-image': 'url(https://images.pexels.com/photos/242616/pexels-photo-242616.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=125&w=210)' }"
          class="card-body"
        >
          <img
            :src="publicKeep.img || 'https://images.unsplash.com/photo-1516962126636-27ad087061cc?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=634&q=80'"
            style="width:100%"
          />
          <h3>{{publicKeep.name}}</h3>
          <h5>{{publicKeep.description}}</h5>
          <button @click.prevent="viewKeep(publicKeep.id)" class="btn-info">
            <router-link
              class="routerlink"
              :to="{ name: 'Keep', params: { id: publicKeep.id } }"
            >View</router-link>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "keepcard",
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    }
  },
  async mounted() {
    await this.$store.dispatch("getPublicKeeps");
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    async viewKeep(KeepId) {
      console.log("This is your keep Id to view", KeepId);
      await this.$store.dispatch("getActiveKeep", KeepId);
      let update = this.$store.state.activeKeep;
      update.views++;
      console.log(update);
      console.log(update.views);
      await this.$store.dispatch("editKeep", update);
      console.log(
        "this is your updated keep from card",
        this.$store.state.activeKeep
      );
    }
  }
};
</script>
<style>
.card {
  box-shadow: 0 6px 9px 0 rgba(0, 0, 0, 0.2);
  transition: 0.3s;
}

.card:hover {
  box-shadow: 0 16px 24px 0 rgba(0, 0, 0, 0.2);
}

.card-body {
  padding: 3px 16px;
}

.routerlink {
  color: #fff;
}
.routerlink:hover {
  color: rgb(48, 226, 25);
}
</style>