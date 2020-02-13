<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col">
        <h1>{{activeKeep.name}}</h1>
        <img :src="activeKeep.img" alt="Image Goes Here" />
        <h3>{{activeKeep.description}}</h3>
        <h1>
          <i @click.prevent="deleteKeep(activeKeep.id)" class="fa fa-trash"></i>
          <div class="dropdown">
            <button
              class="btn btn-secondary dropdown-toggle"
              type="button"
              id="dropdownMenuButton"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
            >Add to Vault</button>
            <div
              class="dropdown-menu"
              aria-labelledby="dropdownMenuButton"
              v-for="(Vault) in vaults"
              :key="Vault.id"
            >
              <a class="dropdown-item" @click.prevent="addVaultKeep(Vault.id)">{{Vault.name}}</a>
            </div>
          </div>
        </h1>
      </div>
    </div>
  </div>
</template>

<script>
import router from "../router";
export default {
  name: "Keep",
  data() {
    return {
      newVaultKeep: {
        vaultId: "",
        keepId: ""
      }
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    activeKeep() {
      return this.$store.state.activeKeep;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  async mounted() {
    await this.$store.dispatch("getActiveKeep", this.$route.params.id);
    console.log("this is active", activeKeep);
  },
  components: {},
  async mounted() {
    await this.$store.dispatch("getVaults");
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    async deleteKeep(id) {
      await this.$store.dispatch("deleteKeep", id);
      router.push({ name: "home" });
    },
    async addVaultKeep(id) {
      this.newVaultKeep = {
        vaultId: id,
        keepId: this.$route.params.id
      };
      await this.$store.dispatch("addVaultKeep", this.newVaultKeep);
      this.newVaultKeep = {
        vaultId: "",
        keepId: ""
      };
    }
  }
};
</script>
