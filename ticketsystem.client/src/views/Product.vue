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
      slaMetrics: [],
      slaTarget: "",
      qnaList: [],
    };
  },
  computed: {
    productId() {
      return this.id;
    },
  },
    methods: {
      async fetchSLA() {
        try {
          const response = await fetch(`https://localhost:7253/Product/productSLA`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: this.productId, Desc: "", Name: "" }),
          });

          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }

          const slaData = await response.json();

          this.uptime = slaData.sla[0].uptime || 'N/A';
          this.resolutionTime = slaData.sla[0].resolution_Time || 'N/A';
          this.responseTime = slaData.sla[0].respone_Time || 'N/A';

        } catch (error) {
          console.error('Error fetching SLA:', error);
        }
      },
      
      async fetchQnA() {
        try {
          const response = await fetch(`https://localhost:7253/Product/productQNAList`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: this.productId, Desc: "", Name: "" }),
          });

          if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
          }

          const qnaData = await response.json();
          console.log(qnaData.qna);
          this.qnaList = qnaData.qna

        } catch (error) {
          console.error('Error fetching SLA:', error);
        }
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
