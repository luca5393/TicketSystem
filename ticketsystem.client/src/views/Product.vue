<template>
  <AppHeader />

  <div class="wrapper">
    <h1 class="product-heading">Product {{ productId }}</h1>

    <div v-if="section === 'qna'">
      <h2>QnA Section</h2>
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
          <span class="uptime-value">{{ uptime || 'Loading...' }}</span>
        </div>

        <div class="sla-metrics">
          <label class="sla-label">Current SLA Metrics:</label>
          <ul class="sla-list">
            <li><strong>Resolution Time:</strong> {{ resolutionTime || 'N/A' }}</li>
            <li><strong>Response Time:</strong> {{ responseTime || 'N/A' }}</li>
          </ul>
        </div>
      </div>
    </div>

    <div v-else>
      <h2>Default Section (Redirected to QnA)</h2>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Product",
    props: {
      section: String,
      productId: Number,
    },
    data() {
      return {
        uptime: '',
        resolutionTime: '',
        responseTime: '',
        qnaList: [],
      };
    },
    mounted() {
      console.log("Product ID:", this.productId); // Log the productId to verify it’s being passed
      if (this.section === 'sla') {
        this.fetchSLA();
      } else if (this.section === 'qna') {
        this.fetchQnA();
      }
    },
    methods: {
      async fetchSLA() {
        try {
          const response = await fetch(`https://localhost:7253/Product/productSLA`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: this.productId }),
          });

          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }

          const slaData = await response.json();
          console.log('SLA Data:', slaData); // Log the SLA data to check its structure

          this.uptime = slaData.uptime || 'N/A';
          this.resolutionTime = slaData.resolution_time || 'N/A';
          this.responseTime = slaData.response_time || 'N/A';

        } catch (error) {
          console.error('Error fetching SLA:', error);
        }
      },
      async fetchQnA() {
        this.qnaList = [
          { question: 'Sample Question 1?', answer: 'Sample Answer 1' },
          { question: 'Sample Question 2?', answer: 'Sample Answer 2' },
        ];
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

  .product-heading {
    font-size: 24px;
    font-weight: bold;
    margin-bottom: 20px;
    text-align: center;
  }

  .uptime-info,
  .sla-metrics {
    margin-bottom: 15px;
  }

  .sla-list {
    list-style: none;
    padding: 0;
    margin: 0;
  }
</style>
