<template>
  <AppHeader />

  <div class="wrapper">
    <h1>Product {{ productId }}</h1>

    <div v-if="section === 'qna'">
      <h2>QnA Section</h2>
    </div>

    <div v-else-if="section === 'sla'">
      <h2>SLA Section</h2>
    </div>

    <div v-else>
      <h2>Default Section (Redirected to QnA)</h2>
    </div>
  </div>
</template>

<script>
export default {
  // eslint-disable-next-line vue/multi-word-component-names
  name: 'Product',
  props: {
    id: {
      type: String,
      required: true,
    },
    section: {
      type: String,
      default: 'qna',
    },
  },
  computed: {
    productId() {
      return this.id;
    },
  },
};
</script>


<style scoped>

.wrapper {
  width: 60vh;
  height: 100vh;
  padding: 20px;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

</style>
