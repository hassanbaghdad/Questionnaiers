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
    class questionnaires_states_Analytics_approval
    {
        db db = new db();
        string now = DateTime.Now.ToString("yyyy/MM/dd h:mm tt");
        public DataTable get_analytics_for_sections(string year , string month)
        {
            string query = "";
            DataTable dt = new DataTable();

            if (year == "All" && month == "All")
            {
                query = "SELECT * FROM statistics_months_history order by year,month ASC";
                SqlCommand cmd = new SqlCommand(query, db.sqlcon());
                cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            
            if (year != "All" && month == "All")
            {
                query = "SELECT * FROM statistics_months_history WHERE year=@year";
                SqlCommand cmd = new SqlCommand(query, db.sqlcon());
                cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
               
            }
            if (year != "All" && month != "All")
            {
                query = "SELECT * FROM statistics_months_history WHERE year=@year AND month=@month";
                SqlCommand cmd = new SqlCommand(query, db.sqlcon());
                cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd.Parameters.Add("@month", SqlDbType.Int).Value = Convert.ToInt32(month);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            if (year == "All" && month != "All")
            {
                query = "SELECT * FROM statistics_months_history WHERE month=@month";
                SqlCommand cmd = new SqlCommand(query, db.sqlcon());
                cmd.Parameters.Add("@month", SqlDbType.Int).Value = Convert.ToInt32(month);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            
            return dt;

        }
      
        public DataTable get_analytics_with_months(string year , int month)
        {

            DataTable dt = new DataTable();
            string query = "SELECT * FROM statistics_sections_history WHERE year=@year AND month=@month";
            SqlCommand cmd = new SqlCommand(query, db.sqlcon());
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;

            SqlDataAdapter adapter =  new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            //MessageBox.Show(year + " " + month.ToString() + " Rows:" + dt.Rows.Count.ToString());
            return dt;
        }


        public List<string> dropdown_year_and_month_for_analytics(string col)
        {
            List<string> dropdown_list = new List<string>();
            DataTable _dt = new DataTable();
            string query = "SELECT DISTINCT "+col+" FROM statistics_months_history";
            SqlCommand cmd = new SqlCommand(query, db.sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(_dt);
            dropdown_list.Add("All");
            foreach (DataRow row in _dt.Rows)
            {
              dropdown_list.Add(row[0].ToString());
                 
            }
            return dropdown_list;
        }

        //Delete Month Aproval
        public bool deleteMonthApproval(string year , int month)
        {
            bool delete_state = false;
            try
            {
                string delete = "DELETE FROM statistics_months_history WHERE year=" + year + " AND month=" + month;
                SqlCommand cmd_delete = new SqlCommand(delete, db.sqlcon());
                cmd_delete.ExecuteNonQuery();
                delete_state = true;

            }catch(Exception e)
            {
                MessageBox.Show("Error in Analytices class -> delete month approval function ->say:"+e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return delete_state;
            


        }
     
        ////////////////////////////////////////////////////
        //////////////////   Approval   ////////////////////
        ////////////////////////////////////////////////////

        //جلب اخر شهر معتمد ومقارنته مع الشهر المراد اللغاء اعتماده
        public bool get_last_month(string year , int month)
        {
            string query3 = "SELECT * FROM statistics_months_history WHERE complated='تم الاعتماد' order by year , month ASC";
            DataTable dt3 = new DataTable();
            SqlCommand cmd3 = new SqlCommand(query3, db.sqlcon());
            SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
            adapter3.Fill(dt3);
            string yearDB ="";
            int monthDB=0;
            foreach(DataRow row in dt3.Rows)
            {
                yearDB = row["year"].ToString();
                monthDB = Convert.ToInt32(row["month"].ToString());

            }

            if (year == yearDB && month == monthDB)
            {
                return true;
            }
            
            return false;
        }
        public bool unApproval(string year , int month)
        {
            if(get_last_month(year,month))
            {
                string update = "UPDATE statistics_months_history SET complated='غير معتمد' WHERE year=" + year + " AND month=" + month;
                SqlCommand cmd_update = new SqlCommand(update, db.sqlcon());
                cmd_update.ExecuteNonQuery();
                MessageBox.Show("تم اللغاء الاعتماد بنجاح", "تم الالغاء", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("عفوا لايمكن اللغاء اعتماد هذا الشهر", "عفواً", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            
        }
        public bool Approval(string year , int month)
        {
            if(checkApproval(year,month)==true)
            {
                 MessageBox.Show("عفوا هذا الشهر تم اعتماده سابقاً","عفواً", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return false;
            }

            bool Approval = false;
            string query = "SELECT id FROM statistics_sections_history WHERE year=" + year + " AND month=" + month;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, db.sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            int count_all = dt.Rows.Count;

           
            string query2 = "SELECT id FROM statistics_sections_history WHERE year=" + year + " AND month=" + month + " AND approve_state='تم الموافقة'";
            DataTable dt2 = new DataTable();
            SqlCommand cmd2 = new SqlCommand(query2, db.sqlcon());
            SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
            adapter2.Fill(dt2);
            int count = dt2.Rows.Count;

            //MessageBox.Show(count_all.ToString());
            //MessageBox.Show(count.ToString());
            
          
            if (count == count_all)
            {
                if (check_last_month(year, month ,"approval") == true)
                {
                    Approval = true;
                    string update = "UPDATE statistics_months_history SET complated='تم الاعتماد' , date_approval='" + now + "' WHERE year=" + year + " AND month=" + month;
                    SqlCommand cmd_update = new SqlCommand(update, db.sqlcon());
                    cmd_update.ExecuteNonQuery();
                    MessageBox.Show("تم الاعتماد بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }else
            {
                MessageBox.Show("يرجى اكمال بقسة الاقسام اولاً", "عفوا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

          
            return Approval;

        }
        //فحص الشهر السابق
        public bool check_last_month(string year , int month ,string checkFrom) //2021-01
        {

            if(checkFirst(year,month) ==false)
            {
                int last_month = 0;
                if(month ==1)
                {
                     last_month = 12;
                     int _year = Convert.ToInt32(year) - 1;
                     year = _year.ToString();
                }
                else
                {
                     last_month = month - 1;
                }
                //MessageBox.Show(year + " " + last_month.ToString());
                
                string query3 = "SELECT * FROM statistics_months_history WHERE year=" + year + " AND month=" + last_month + "AND complated='تم الاعتماد'";
                DataTable dt3 = new DataTable();
                SqlCommand cmd3 = new SqlCommand(query3, db.sqlcon());
                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                adapter3.Fill(dt3);
                int last_month_approval = dt3.Rows.Count; //must be 1 => approval last month

                //MessageBox.Show(last_month_approval.ToString());
                if(checkFrom =="approval")
                {
                    if (last_month_approval < 1)
                    {
                        MessageBox.Show("عفوا يرجى اعتماد الشهر السابق اولا", "انتباه !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;

                    }
                }
                else
                {
                    if (last_month_approval ==1)
                    {
                        MessageBox.Show("عفوا يرجى اعتماد الشهر السابق اولا", "انتباه !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;

                    }
                }
                

                return true;
            }
            return true;
           
        }

                //هل هذا الاعتماد هو اول اعتماد في قاعدة البيانات؟
                public bool checkFirst(string year, int month)
                {
                    bool First = false;
                    string query = "SELECT TOP 1 id FROM statistics_months_history WHERE year ="+year+" AND month="+month +" AND complated='تم الاعتماد' order by year,month";
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand(query, db.sqlcon());
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    if (dt.Rows.Count ==0)
                    {
                        First = true;
                    }
                    return First;
                }
        //["1" , "2" ,"3","5"]
        //  0     1    2   3

        //معتمد او غير معمد
        public bool checkApproval(string year , int month)
        {
            bool Approval = false;
            string query = "SELECT id FROM statistics_months_history WHERE complated='تم الاعتماد'"+" AND year=" + year + " AND month=" + month ;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, db.sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            if(dt.Rows.Count>0)
            {
                Approval = true;
            }
            return Approval;
        }

        //جلب تاريخ الاعتماد
        public string getDateApproval(string year, int month)
        {
        
            string query = "SELECT date_approval FROM statistics_months_history WHERE complated='تم الاعتماد'" + " AND year=" + year + " AND month=" + month;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, db.sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            string date = "";
            foreach(DataRow row in dt.Rows)
            {
                date = row[0].ToString();
            }
            //MessageBox.Show(date);
            return date;
        }
        //حذف قسم من الاعتماد
        public bool deleteSectionApproval(string year, int month,int section_id)
        {
            bool delete_state = false;
            try
            {
                string delete = "DELETE FROM statistics_sections_history WHERE year=" + year + " AND month=" + month +" AND section_id="+section_id;
                SqlCommand cmd_delete = new SqlCommand(delete, db.sqlcon());
                cmd_delete.ExecuteNonQuery();
                delete_state = true;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in Analytices class -> delete month approval function ->say:" + e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return delete_state;



        }
    }
}
