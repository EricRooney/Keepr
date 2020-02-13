import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "./router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    activeKeep: {},
    vaults: []
  },
  mutations: {
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps;
      console.log("Look", state.publicKeeps);
    },
    setActiveKeep(state, keep) {
      state.activeKeep = keep;
      console.log("Made it to active keep", state.activeKeep);
    },
    setVaults(state, vaults) {
      state.vaults = vaults;
    }
  },
  actions: {
    setBearer({}, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    async getPublicKeeps({ commit, dispatch }) {
      let res = await api.get("keeps");
      console.log("these are all of the keeps", res.data);

      commit("setPublicKeeps", res.data);
    },
    async getActiveKeep({ commit, dispatch }, id) {
      let res = await api.get("keeps/" + id);
      console.log("this is your active keep", res.data);
      commit("setActiveKeep", res.data);
    },
    async editKeep({ commit, dispatch }, updatedKeep) {
      let res = await api.put("keeps/" + updatedKeep.id, updatedKeep);
      console.log("this is your updated active keep", res.data);
      commit("setActiveKeep", res.data);
    },
    async deleteKeep({ commit, dispatch }, id) {
      await api.delete("keeps/" + id);
    },
    async getVaults({ commit, dispatch }) {
      let res = await api.get("vaults");
      console.log("these are your vaults", res.data);
      commit("setVaults", res.data);
    }
  }
});
