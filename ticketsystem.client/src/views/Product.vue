<template>
  <AppHeader />

  <div>
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


<style scoped></style>