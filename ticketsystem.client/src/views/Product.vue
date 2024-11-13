<template>
  <AppHeader />

  <div class="wrapper">
    <h1 class="product-heading">Product {{ productId }}</h1>

    <div v-if="section === 'qna'">
      <h2>QnA Section</h2>

      <!-- Q&A List -->
      <ul class="qna-list">
        <li v-for="(qna, index) in qnaList" :key="index" class="qna-item">
          <strong>Q: {{ qna.question }}</strong>
          <p>A: {{ qna.answer }}</p>
        </li>
      </ul>
    </div>

    <div v-else-if="section === 'sla'">
      <div class="uptime">
        <h2>SLA Section</h2>

        <div class="uptime-info">
          <label class="uptime-label">Product Uptime:</label>
          <span class="uptime-value">{{ uptime }}</span>
        </div>

        <div class="sla-metrics">
          <label class="sla-label">Current SLA Metrics:</label>
          <ul class="sla-list">
            <li v-for="(metric, index) in slaMetrics" :key="index">
              <strong>{{ metric.name }}:</strong> {{ metric.value }}
            </li>
          </ul>
        </div>

        <div class="sla-targets"></div>
      </div>
    </div>

    <div v-else>
      <h2>Default Section (Redirected to QnA)</h2>
    </div>
  </div>
</template>

<script>
export default {
  // eslint-disable-next-line vue/multi-word-component-names
  name: "Product",
  props: {
    id: {
      type: String,
      required: true,
    },
    section: {
      type: String,
      default: "qna",
    },
  },
  data() {
    return {
      uptime: "99.9%",
      slaMetrics: [
        { name: "Response Time", value: "200 ms" },
        { name: "Availability", value: "99.95%" },
        { name: "Downtime", value: "2 hours/month" },
      ],
      slaTarget: "",
      qnaList: [
        { question: "What is the warranty period for this product?", answer: "The warranty period is 2 years from the purchase date." },
        { question: "How can I reset the product settings?", answer: "To reset, press and hold the reset button for 10 seconds." },
        { question: "Is this product compatible with other brands?", answer: "Yes, it is compatible with most brands in the market." },
        { question: "What is the power consumption?", answer: "The product consumes 50 watts during operation." },
        { question: "Can I use this product outdoors?", answer: "This product is designed for indoor use only." },
      ],
    };
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
  width: 100vw;
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

h1 {
  margin-right: 7vw;
}

.product-heading {
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 20px;
  text-align: center;
}

.uptime {
  margin-right: 1vw;
  text-align: left;
}

.uptime-info,
.sla-metrics,
.sla-targets,
.qna-list {
  margin-bottom: 15px;
}

.uptime-label,
.sla-label,
.sla-target-label {
  display: flex;
  font-weight: bold;
  margin-bottom: 5px;
}

.uptime-value {
  font-size: 16px;
}

.sla-list,
.qna-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.sla-list li,
.qna-item {
  margin-bottom: 20px;
}
</style>
