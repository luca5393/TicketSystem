import { createClient } from '@supabase/supabase-js';

const supabaseUrl = 'https://udrzymhgvoinbnrombuj.supabase.co';
const supabaseKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVkcnp5bWhndm9pbmJucm9tYnVqIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzA0NDU4MTAsImV4cCI6MjA0NjAyMTgxMH0.8e3KcbTlAyrfFMZmdvSYqV_AvcGVCXp2CCllPdWfYWk';

const supabase = createClient(supabaseUrl, supabaseKey);

// Listen for authentication state changes
supabase.auth.onAuthStateChange((event, session) => {
    if (event === 'SIGNED_IN' || event === 'SIGNED_OUT') {
        console.log('Auth state changed:', event, session);
    }
});

export default supabase;
