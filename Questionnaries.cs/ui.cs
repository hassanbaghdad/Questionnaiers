using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Questionnaries.cs
{
    class ui
    {
       
        db db = new cs.db();
        public  float sum1, sum2, sum3, sum4 = 0;
        public int count1, count2, count3, count4 = 0;
        public int n = 0;
        public float avarge1, avarge2, avarge3, avarge4 = 0;
        
        public void Avarge(string year , int month)
        {

          


            DataTable dt = new DataTable();
            
            string query = "SELECT * FROM tbl_months  WHERE  year="+year +" AND month="+month;


            SqlCommand cmd = new SqlCommand(query, db.sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            n = dt.Rows.Count;
            List<String> categories = new List<string>(); 
           
            
            // Months Foreach
            foreach(DataRow row_month in dt.Rows)
            {

                int    _id            =    Convert.ToInt32(row_month["id"].ToString());

                string get_answers_from_log = "SELECT * FROM tbl_questions_log WHERE answer = 1 AND id_exam="+_id;
                SqlCommand cmd_get_from_log = new SqlCommand(get_answers_from_log, db.sqlcon());
                SqlDataAdapter adapter_log = new SqlDataAdapter(cmd_get_from_log);
                DataTable _dt_log = new DataTable();
                adapter_log.Fill(_dt_log);
               //questions 16
                

                // Questions Foreach
                foreach(DataRow row_q_log in _dt_log.Rows)
                {
                    
                    //MessageBox.Show("section_id:  " +row_q_log["section_id"].ToString()+ " Count: " +c.ToString());
                    int cat_id = Convert.ToInt32(row_q_log["category_id"].ToString());

                    int event_assetment_after_edit = Convert.ToInt32(row_q_log["event_assetment_after_edit"].ToString());
                    int event_assetment_final = 0;

                    if(event_assetment_after_edit ==0)
                    {
                        event_assetment_final = Convert.ToInt32(row_q_log["event_assetment"].ToString());
                    }
                    else
                    {
                        event_assetment_final = event_assetment_after_edit;
                    }






                    if (cat_id == 1)
                    {

                        sum1 += event_assetment_final;
                        if(event_assetment_final > 0)
                        {
                            count1++;
                        }
                        
                    }
                    if (cat_id == 2)
                    {

                        sum2 += event_assetment_final;
                        if (event_assetment_final > 0)
                        {
                            count2++;
                        }
                    }
                    if (cat_id == 3)
                    {

                        sum3 += event_assetment_final;
                        if (event_assetment_final > 0)
                        {
                            count3++;
                        }
                    }
                    if (cat_id == 4)
                    {

                        sum4 += event_assetment_final;
                        if (event_assetment_final > 0)
                        {
                            count4++;
                        }
                    }

                   
                    
                }//End Questions Foreach
                
                    if (count1 > 0)
                    {
                        avarge1 = sum1 / count1;
                    }
                    else
                    {
                        avarge1 = 0;
                    }
                    if (count2 > 0)
                    {
                        avarge2 = sum2 / count2;
                    }
                    else
                    {
                        avarge2 = 0;
                    }
                    if (count3 > 0)
                    {
                        avarge3 = sum3 / count3;
                    }
                    else
                    {
                        avarge3 = 0;
                    }
                    if (count4> 0)
                    {
                        avarge4 = sum4 / count4;
                    }
                    else
                    {
                        avarge4 = 0;
                    }
                   
                    
                 

                float[] avarges = new float[5];
                avarges[0] = 0;
                avarges[1]=avarge1;
                avarges[2]=avarge2;
                avarges[3]=avarge3;
                avarges[4]=avarge4;

                

              
                int section_id = Convert.ToInt32((row_month["section_id"].ToString()));
                int month_id = Convert.ToInt32((row_month["id"].ToString()));
                for(int i = 1 ; i <=4 ; i++)
                {
          //Insert To Questions Total
                    int category_id = i;
                    insert_to_tbl_statistics_questions_total(section_id, category_id, month_id, avarges[i], year, month);
             


                }

          //Insert To Section Total

                int avarge_count = 0;
                for (int x = 1; x <= 4; x++ )
                {
                    if (avarges[x] > 0)
                    {
                        avarge_count++;
                       
                    }
                }
                
          
                float total_avarge = (avarge1+avarge2+avarge3+avarge4) / avarge_count;
                if(total_avarge > 0)
                {
                    insert_to_tbl_statistics_section_total(section_id, month_id, total_avarge,year,month);
                }
                else
                {
                    total_avarge = 0;
                    insert_to_tbl_statistics_section_total(section_id, month_id, total_avarge, year, month);
                }
                

                
               
               

                avarge1 = 0;
                avarge2 = 0;
                avarge3 = 0;
                avarge4 = 0;
                sum1 = 0;
                sum2 = 0;
                sum3 = 0;
                sum4 = 0;
                count1 = 0;
                count2 = 0;
                count3 = 0;
                count4 = 0;
                avarge_count = 0;
            
           }
            //End Months Foreach
            
            //function datatable tbl_total_category_statistics
            // calculate 
            /*
             * 1- Create DataTable in total_questions
             * 2-foreach on all table
             * 
             */
           // MessageBox.Show("Success");
            
            
            
            //category table

            //1 - foreach for category 
            //2 - 8 varables sum , count 
            //3 - avarage = 4 varables / count
            //4 - insert function
            category_total(year, month);
        }

       public float c_sum1, c_sum2, c_sum3, c_sum4 = 0;
       public int c_count1, c_count2, c_count3, c_count4 = 0;
       public int c_n = 0;
       public float c_avarge1, c_avarge2, c_avarge3, c_avarge4 = 0;
       public float[] c_avarges = new float[5];
        public void category_total(string year , int month)
        {
            
        
            string query = "SELECT * FROM tbl_statistics_questions_total WHERE year=" + year + " AND month=" + month;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, db.sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach(DataRow row in dt.Rows)
            {
                int cat_id = Convert.ToInt32(row["category_id"].ToString());

                if (cat_id == 1)
                {
                    float av = Convert.ToSingle(row["section_avarge"].ToString());
                    c_sum1 += av;

                    if (av > 0)
                    {
                        c_count1++;
                    }
                }
                if (cat_id == 2)
                {
                    float av = Convert.ToSingle(row["section_avarge"].ToString());
                    c_sum2 += av;

                    if (av > 0)
                    {
                        c_count2++;
                    }
                }
                if (cat_id == 3)
                {
                    float av = Convert.ToSingle(row["section_avarge"].ToString());
                    c_sum3 += av;

                    if (av > 0)
                    {
                        c_count3++;
                    }
                }
                if (cat_id == 4)
                {
                    float av = Convert.ToSingle(row["section_avarge"].ToString());
                    c_sum4 += av;

                    if (av > 0)
                    {
                        c_count4++;
                    }
                }

                ///////////////
                if (c_count1 > 0)
                {
                    c_avarge1 = c_sum1 / c_count1;
                }
                else
                {
                    c_avarge1 = 0;
                }
                if (c_count2 > 0)
                {
                    c_avarge2 = c_sum2 / c_count2;
                }
                else
                {
                    c_avarge2 = 0;
                }
                if (c_count3 > 0)
                {
                    c_avarge3 = c_sum3 / c_count3;
                }
                else
                {
                    c_avarge3 = 0;
                }
                if (c_count4 > 0)
                {
                    c_avarge4 = c_sum4 / c_count4;
                }
                else
                {
                    c_avarge4 = 0;
                }
                /////////////
                
                c_avarges[0] = 0;
                c_avarges[1] = c_avarge1;
                c_avarges[2] = c_avarge2;
                c_avarges[3] = c_avarge3;
                c_avarges[4] = c_avarge4;

                ////////////
                
               
            }
            for (int i = 1; i <= 4; i++)
            {
                //Insert

                int category_id = i;
                insert_to_tbl_statistics_category_total(category_id, c_avarges[i], year, month);
            }
            
        }




        public void insert_to_tbl_statistics_questions_total(int section_id, int category_id, int month_id, float section_avarge , string year, int month)
        {
            try
            {

                string insert_query = "INSERT INTO tbl_statistics_questions_total (id,category_id,section_id,month_id,section_avarge,year,month) VALUES(@id,@category_id,@section_id,@month_id,@section_avarge,@year,@month)";
                SqlCommand cmd_insert = new SqlCommand(insert_query, db.sqlcon());

                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = db.get_last_id("tbl_statistics_questions_total");
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_insert.Parameters.Add("@month_id", SqlDbType.Int).Value = month_id;
                cmd_insert.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;
                cmd_insert.Parameters.Add("@section_avarge", SqlDbType.Float).Value = section_avarge;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;

                cmd_insert.ExecuteNonQuery();
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        public void insert_to_tbl_statistics_section_total(int section_id, int month_id, float total_avarge, string year, int month)
        {
            //MessageBox.Show(total_avarge.ToString());
            try
            {

                string insert_query = "INSERT INTO tbl_statistics_section_total (id,section_id,month_id,total,year,month) VALUES(@id,@section_id,@month_id,@total_avarge,@year,@month)";
                SqlCommand cmd_insert = new SqlCommand(insert_query, db.sqlcon());

                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = db.get_last_id("tbl_statistics_section_total");
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_insert.Parameters.Add("@month_id", SqlDbType.Int).Value = month_id;
                cmd_insert.Parameters.Add("@total_avarge", SqlDbType.Float).Value = total_avarge;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_insert.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        public void insert_to_tbl_statistics_category_total(int category_id, float category_total_avarge , string year , int month)
        {
            try
            {

                string insert_query = "INSERT INTO tbl_statistics_category_total (id,category_id,category_total,year,month) VALUES(@id,@category_id,@category_total,@year,@month)";
                SqlCommand cmd_insert = new SqlCommand(insert_query, db.sqlcon());

                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = db.get_last_id("tbl_statistics_category_total");
                cmd_insert.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;
                cmd_insert.Parameters.Add("@category_total", SqlDbType.Float).Value = category_total_avarge;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;

                cmd_insert.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                MessageBox.Show("Error in ui class in insert to tbl_statistics_category_total , as : " + err.Message);
            }

        }

        
        public void clear_table(string tbl ,string year , int month)
        {
            //MessageBox.Show("table: " + tbl + " year: " + year + " month: " + month.ToString());
            string clear = "DELETE FROM " + tbl + " WHERE year=" + year + " AND month="+month;
            SqlCommand cmd_clear = new SqlCommand(clear, db.sqlcon());
            cmd_clear.ExecuteNonQuery();
            
        }
    }
}
