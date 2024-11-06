<template>
  <main class="main-fill">
    <router-view></router-view>
  </main>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
.main-fill {
  width: 100%;
  height: 100%;
  display: flex;
}
</style>