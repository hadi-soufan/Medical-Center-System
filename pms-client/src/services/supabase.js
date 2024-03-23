
import { createClient } from '@supabase/supabase-js'
export const supabaseUrl = 'https://meflfyplosqudyuvtdgw.supabase.co'
const supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1lZmxmeXBsb3NxdWR5dXZ0ZGd3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDczOTI1MDUsImV4cCI6MjAyMjk2ODUwNX0.in-oFm79r5mrtFNJC9f6vCwufUGVnH_EVjbTQfXARqc"
const supabase = createClient(supabaseUrl, supabaseKey)

export default supabase;