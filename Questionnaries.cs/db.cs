using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
namespace Questionnaries.cs
{
    class db
    {
        //Public Variables
        string now = DateTime.Now.ToString("yyyy/MM/dd h:mm tt");
        string now2 = DateTime.Now.ToString("yyyy/MM/dd h:mm tt");
        


        //Connection
        public SqlConnection sqlcon()
        {
            SqlConnection sqlcon = new SqlConnection(@"Server=.;Database=questionnaires_db;Trusted_Connection=True;");
            try
            {
                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                    // MessageBox.Show(sqlcon.State.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return sqlcon;
        }

        //Get Last ID
        public int get_last_id(string tbl)
        {
            int count = 0 ;
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM "+ tbl+" ORDER BY id DESC" ;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
               
                count = Convert.ToInt32(row[0].ToString());
            }
           if(tbl =="tbl_months" && count != 0)
           {
               count -= 1;
           }
            return count+1;

        }

        //////////////////////////////////////  Start Sections Methods   //////////////////////////////////////
        //Get All Sections
        public DataTable get_sections()
        {
            DataTable dt = new DataTable();
            String query = "SELECT * FROM tbl_sections";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            adapter.Dispose();

            return dt;
        }
        public DataTable get_branchs()
        {
            DataTable dt = new DataTable();
            String query = "SELECT * FROM tbl_sections WHERE section_type=@branch ORDER BY name DESC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@branch", SqlDbType.VarChar).Value = "branch";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            adapter.Dispose();

            return dt;
        }
        public DataTable get_departments()
        {
            DataTable dt = new DataTable();
            String query = "SELECT * FROM tbl_sections WHERE section_type=@department ORDER BY name DESC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@department", SqlDbType.VarChar).Value = "department";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            adapter.Dispose();

            return dt;
        }

        //Create Section
        public void create_section(string s_name ,string manager,string phone, string s_type, int enable , string address , string user_creater, bool msg)
        {
            try
            {
                String query = "INSERT INTO tbl_sections (id,name,manager,phone,section_type,enable,address,user_creator,created_at) Values(@s_id,@s_name,@manager,@phone,@s_type,@s_enable,@s_address,@s_user_creator,@created_at)";

                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = sqlcon();
                    cmd.Parameters.Add("@s_id", SqlDbType.Int).Value = get_last_id("tbl_sections");
                    cmd.Parameters.Add("@s_name", SqlDbType.VarChar, 50).Value = s_name;
                    cmd.Parameters.Add("@manager", SqlDbType.VarChar, 50).Value = manager;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar, 50).Value = phone;
                    cmd.Parameters.Add("@s_type", SqlDbType.VarChar, 50).Value = s_type;
                    cmd.Parameters.Add("@s_enable", SqlDbType.Int).Value = enable;
                    cmd.Parameters.Add("@s_address", SqlDbType.VarChar, 50).Value = address;
                    cmd.Parameters.Add("@s_user_creator", SqlDbType.VarChar, 50).Value = user_creater;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
                    


                    cmd.ExecuteNonQuery();
                    if (msg == true)
                    {
                        MessageBox.Show("Save Success", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in create section is : " + e.Message);
            }



        }

        //Delete Section
        public void delete_section(int id_section)
        {
            try
            {
                String query = "Delete From tbl_sections Where id =" + id_section;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in edit section is : " + e.Message);
            }


        }

        //Get Section To Edit 
        public DataTable get_section_to_edit(int id_section)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_sections WHERE id=" + id_section;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;

        }
        //Edit Section
        public void edit_section(int id_section , string s_name, string manager, string phone, string s_type, int enable, string address, string user_creator, bool msg)
        {
            try
            {
                String query = "Update tbl_sections SET name=@name,section_type=@type ,address=@address,user_creator=@user_creator,manager=@manager, phone=@phone, enable=@enable WHERE id=@id";

                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.Parameters.AddWithValue("@name", s_name);
                cmd.Parameters.AddWithValue("@manager", manager);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@type", s_type);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@user_creator", user_creator);
                cmd.Parameters.AddWithValue("@enable", enable);
                cmd.Parameters.AddWithValue("@id", id_section);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Edit Success", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in edit section is : " + e.Message);
            }


        }


        //Delete All Questions for Sections
        public void delete_all_question_for_section(int id)
        {
            try
            {
                string query = "DELETE FROM tbl_questions WHERE section_id=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in delete all questions for department is : " + e.Message);
            }
        }

        //Get Last ID Sections
        public int get_last_id_section()
        {
            DataTable dt = new DataTable();
            int index = 0;
            try
            {


                string query = "SELECT TOP 1 * FROM tbl_sections ORDER BY id DESC";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    index = Convert.ToInt32(row[0].ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in get last ID for department is : " + e.Message);
            }




            return index;
        }

        //Push A Section
        public void push_department(int section_id)
        {
            try
            {
                string query = "UPDATE tbl_sections SET enable=1 WHERE id=" + section_id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Pushed .", "Pushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in Push Section is : " + e.Message);
            }

        }
        //UnPush A Section
        public void unpush_department(int section_id)
        {
            try
            {
                string query = "UPDATE tbl_sections SET enable=0 WHERE id=" + section_id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("UnPushed .", "UnPushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in UnPush section is : " + e.Message);
            }

        }

        //Push All Questionnaires to All Sections
        public void push_all_questionnaires()
        {
            try
            {
                string query = "UPDATE tbl_sections SET enable=1";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("All Pushed .", "Pushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in Push All section is : " + e.Message);
            }

        }

        //Unpush All Questionnaires to All Sections
        public void unpush_all_questionnarires()
        {
            try
            {
                string query = "UPDATE tbl_sections SET enable=0";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("All UnPushed .", "UnPushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in UnPush All Sections is : " + e.Message);

            }

        }

        //Get ID Section From Section Name
        public int get_id_section_from_section_name(string section_name)
        {
            int id=0;
            DataTable dt = new DataTable();
            string query = "SELECT id FROM tbl_sections WHERE name=@section_name";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_name",SqlDbType.VarChar).Value = section_name;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach(DataRow row in dt.Rows)
            {
                id = Convert.ToInt32(row["id"].ToString());
            }
            return id;
        }

        //Get Section Name From Section ID
        public string get_section_name_from_id_section(int id)
        {
            string section_name = "";
            DataTable dt = new DataTable();
            string query = "SELECT name FROM tbl_sections WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                section_name = row["name"].ToString();
            }
            return section_name;
        }
        //Get Section ID From Section NAME
        public int get_section_id_from_name_section(string name)
        {
            int section_id = 0;
            DataTable dt = new DataTable();
            string query = "SELECT id FROM tbl_sections WHERE name=@name";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                section_id = Convert.ToInt32(row["id"].ToString());
            }
        
            return section_id;
        }

        //Get Section type From Section ID
        public string get_section_type_from_id_section(int id)
        {
            string section_type = "";
            DataTable dt = new DataTable();
            string query = "SELECT section_type FROM tbl_sections WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                section_type = row["section_type"].ToString();
            }
            return section_type;
        }

        //////////////////////////////////////     End Sections Methods   //////////////////////////////////////











        //////////////////////////////////////     Start Categories Methods ///////////////////////////////////////

        //Get Categories
        public DataTable get_all_cat()
        {
            DataTable dt = new DataTable();
            string query = "SELECT category_name FROM tbl_categories";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        //Create Category
        public void create_category(string category_name)
        {
            try
            {
                string query = "INSERT INTO tbl_categories (id,category_name,created_at) VALUES (@id,@category_name,@created_at)";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("tbl_categories");
                    cmd.Parameters.Add("@category_name", SqlDbType.VarChar, 50).Value = category_name;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime).Value = now;
                    cmd.Connection = sqlcon();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Insert Success .", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in create category error is : " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Delete Category
        public void delete_category(string category_name)
        {
            try
            {
                string query = "DELETE FROM tbl_categories WHERE category_name=@category_name";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    
                    cmd.Parameters.Add("@category_name", SqlDbType.VarChar, 50).Value = category_name;
                    
                    cmd.Connection = sqlcon();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Delete Success .", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in delete category error is : " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Get Category ID 
        public int get_category_id(string category_name)
        {
            int id=0;
            DataTable dt = new DataTable();
            string query = "SELECT id FROM tbl_categories WHERE category_name=@category_name";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@category_name", SqlDbType.VarChar, 50).Value = category_name;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
                id = Convert.ToInt32(row[0].ToString());
            }
            return id;
        }


        //////////////////////////////////////     End Categories Methods ////////////////////////////////////////








        //////////////////////////////////////     Start Questions Methods ////////////////////////////////////////

        //Get All Questions For A Section
        public DataTable get_questions_section(int id_section)
        {

            DataTable dt = new DataTable();
            String query = "SELECT * FROM tbl_questions WHERE section_id=" + id_section + "ORDER BY section_q_secunce DESC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            adapter.Dispose();

            return dt;

        }


        //Create Question For Section
        public void create_question_for_section(string text, string notic,string q_type, int id_section)
        {
            try
            {
                String query = "INSERT INTO tbl_questions (id,section_q_secunce,q_text,category_id,q_type,section_id,created_at) Values(@id,@section_q_secunce,@q_text,@category_id,@q_type,@id_section,@created_at)";

                using (SqlCommand cmd = new SqlCommand(query))
                {


                    cmd.Connection = sqlcon();
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("tbl_questions");
                    cmd.Parameters.Add("@section_q_secunce", SqlDbType.Int).Value = get_last_secunce_no_for_question(id_section);

                    cmd.Parameters.Add("@q_text", SqlDbType.Text).Value = text;
                    cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = get_category_id(q_type);
                    cmd.Parameters.Add("@q_type", SqlDbType.Text).Value = q_type;
                    cmd.Parameters.Add("@id_section", SqlDbType.Int, 50).Value = id_section;
                    cmd.Parameters.Add("@notic", SqlDbType.Text).Value = notic;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Save Successfully", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in create question for section is : " + e.Message);
            }
        }

        //Edit question
        public void edit_question(string text, string notic,string type,int id_section,int q_id)
        {
            try
            {

                string query = "UPDATE tbl_questions SET q_text=@q_text ,q_type=@type,notic=@notic , updated_at=@updated_at WHERE section_id=@section_id AND id=@q_id";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.Parameters.Add("@q_text", SqlDbType.Text).Value = text;
                cmd.Parameters.Add("@notic", SqlDbType.Text).Value = notic;
                cmd.Parameters.Add("@type", SqlDbType.VarChar,50).Value = type;
                cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = id_section;
                cmd.Parameters.Add("@q_id", SqlDbType.Int).Value = q_id;
                cmd.Parameters.Add("@updated_at", SqlDbType.DateTime).Value = now;


                cmd.ExecuteNonQuery();
                MessageBox.Show("Edit Success .", "Done .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception e)
            {
                MessageBox.Show("Error in db class in edit question is: "+ e.Message, "Done .", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //Delelte Question 
        public void delete_question(int secunce_no , int section_id )
        {
            try
            {
                string query = "DELETE FROM tbl_questions where section_id=@section_id  and section_q_secunce=@secunce_no";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd.Parameters.Add("@secunce_no", SqlDbType.Int).Value = secunce_no;

                cmd.ExecuteNonQuery();
                update_secunce_questions_for_section(section_id,secunce_no);
                MessageBox.Show("Delete Successfully", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in delete question for section is : " + e.Message);
            }

        }
        //Delelte All Question For Section
        public void delete_all_question(int section_id)
        {
            try
            {
                string query = "DELETE FROM tbl_questions where section_id=" + section_id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Successfully", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in delete question for section is : " + e.Message);
            }

        }
        //Save questions as month
        public void save_to_months_tbl(int month, string year, int section_id)
        {
            string insert = "INSERT INTO tbl_months (id,month,year,section_id,questionnaier_section_no,created_at) VALUES(@id , @month ,@year,@section_id,@questionnaier_section_no,@created_at)";
            try
            {
                SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());
                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("tbl_months")+1;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int, 50).Value = section_id;
                cmd_insert.Parameters.Add("@questionnaier_section_no", SqlDbType.Int, 50).Value = get_last_no_questionnaier_for_section(section_id, true);

                cmd_insert.Parameters.Add("@created_at", SqlDbType.VarChar, 50).Value = now2;
           

                cmd_insert.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in save questions in tbl months : " + e.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Send Questions For Web
        public bool check_has_send_to_web(int section_id , string year ,int month)
        {
           
            bool has_send = false;
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_months WHERE section_id="+section_id+" AND year="+year+" AND month="+month.ToString();
            SqlCommand cmd= new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            
            if(dt.Rows.Count > 0 )
            {
                has_send = true;
            }
            return has_send;
        }
        public void send_questions_for_web(int id_section,string year , int month)
        {
            save_to_months_tbl(month, year,  id_section);
            //1-Get Questions
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_questions WHERE section_id=@id_section";
            string insert = "INSERT INTO tbl_questions_log (id,questionnaier_section_no , id_q ,date_send,q_type,q_text,section_id,year,month,category_id,id_exam) VALUES(@id_log ,@questionnaier_section_no, @id_q ,@date_send,@q_type,@q_text,@section_id,@year,@month,@category_id,@id_exam)";

            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@id_section", SqlDbType.Int).Value = id_section;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
                try
                {
                    SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());
                    cmd_insert.Parameters.Add("@id_log", SqlDbType.Int).Value = get_last_id("tbl_questions_log");
                    cmd_insert.Parameters.Add("@questionnaier_section_no", SqlDbType.Int).Value = get_last_no_questionnaier_for_section(id_section,false);

                    cmd_insert.Parameters.Add("@id_q", SqlDbType.Int).Value = Convert.ToInt32(row["section_q_secunce"].ToString());
                    cmd_insert.Parameters.Add("@date_send", SqlDbType.VarChar).Value = now;
                    cmd_insert.Parameters.Add("@q_type", SqlDbType.VarChar, 50).Value = row["q_type"].ToString();
                    cmd_insert.Parameters.Add("@q_text", SqlDbType.Text).Value = row["q_text"].ToString();
                    //cmd_insert.Parameters.Add("@section_type", SqlDbType.VarChar, 50).Value = row["section_type"].ToString();
                    cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = Convert.ToInt32(row["section_id"].ToString());
                    cmd_insert.Parameters.Add("@category_id", SqlDbType.Int).Value = Convert.ToInt32(row["category_id"].ToString());
                    cmd_insert.Parameters.Add("@year", SqlDbType.VarChar, 50).Value = year;
                    cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    cmd_insert.Parameters.Add("@id_exam", SqlDbType.Int).Value = get_last_id("tbl_months");

                    cmd_insert.ExecuteNonQuery();
                    
                }catch(Exception e)
                {
                    MessageBox.Show("Error in db class in send questions to web is : "+e.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //MessageBox.Show("Questions for year: " + year + " and month: " + month + " has been send .", "Done ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //get last no for questionnaier section in tbl_months
        public int get_last_no_questionnaier_for_section(int section_id, bool secuince)
        {
            //TOP 1 * FROM "+ tbl+" ORDER BY id DESC
            int last_no = 0;
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 questionnaier_section_no FROM tbl_months WHERE section_id=@section_id ORDER BY questionnaier_section_no DESC ";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                last_no = Convert.ToInt32(row["questionnaier_section_no"].ToString());
            }
            if (secuince == true)
            {
                last_no += 1;
            }
          //  MessageBox.Show(last_no.ToString());
            return last_no;

        }
        //get last secunce no question
        public int get_last_secunce_no_for_question(int section_id)
        {
            //TOP 1 * FROM "+ tbl+" ORDER BY id DESC
            int last_no = 0;
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 section_q_secunce FROM tbl_questions WHERE section_id=@section_id ORDER BY section_q_secunce DESC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                last_no = Convert.ToInt32(row["section_q_secunce"].ToString());
            }
            
            
            return last_no+1;

        }

       

        // update secunce no for question
        public void update_secunce_questions_for_section(int section_id , int secunce_no)
        {
            
            string query_all_question_in_section = "SELECT * FROM tbl_questions WHERE section_id=@section_id";
            SqlCommand cmd = new SqlCommand(query_all_question_in_section, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            
            foreach(DataRow row in dt.Rows)
            {
                int current_secunce_in_db = Convert.ToInt32(row["section_q_secunce"].ToString());
                int current_id = Convert.ToInt32(row["id"].ToString());

                if(current_secunce_in_db > secunce_no )
                {
                    try
                    {
                        string update = "UPDATE tbl_questions SET section_q_secunce=@section_q_secunce WHERE section_id=@section_id AND id=@id";
                        SqlCommand cmd_update = new SqlCommand(update, sqlcon());
                        cmd_update.Parameters.Add("@section_q_secunce", SqlDbType.Int).Value = current_secunce_in_db - 1;
                        cmd_update.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                        cmd_update.Parameters.Add("@id", SqlDbType.Int).Value = current_id;

                        cmd_update.ExecuteNonQuery();
                    }catch(Exception e)
                    {
                        MessageBox.Show("Error in db class in ipdate_secunce_question", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                
            }
            
               
                
            
            MessageBox.Show("updated all secunces success");
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////START STATISTCIS HISTORY////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // insert all sections in tbl statistics section history
        public void insert_all_sections_to_tbl_statistics_section_history(string year , int month)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_sections";
            SqlCommand cmd_get = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd_get);
            adapter.Fill(dt);
            empty(year,month);
            foreach(DataRow row in dt.Rows)
            {
                
                int section_id_in_foreach = Convert.ToInt32(row["id"].ToString());
                string section_type = row["section_type"].ToString();
                string section_name = row["name"].ToString();
                List<String> states =  check_section_if_send_to_tbl_month_or_no(section_id_in_foreach, year, month);
                
                string send_state = states[0];
                string answer_state = states[1];
                string approve_state = states[2];

                //if (check_if_inserted_in_tbl_sections_history(section_id_in_foreach, year, month) == false)
               // {
                    //MessageBox.Show(send_state);

                    insert_to_sections_history(year, month, section_id_in_foreach, section_type, section_name, send_state, answer_state, approve_state);
               // }


            }
        }

        //check section if send or no in tbl month
        public List<String> check_section_if_send_to_tbl_month_or_no(int section_id, string year, int month)
        {
            
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_months WHERE section_id=" + section_id + " AND year=" + year + " AND month=" + month;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            List<String> state = new List<string>();
            state.Clear();
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    //MessageBox.Show("id:" + section_id + " ok");
                    
                        state.Add("تم الارسال");
                        state.Add(row["answer_state"].ToString());
                        state.Add(row["approve"].ToString());

                }
            }
            else
            {
                state.Add("لم يتم الارسال");
                state.Add("-");
                state.Add("-");
            }

            return state;
            
        }

        //Check if inserted in sections_history tbl
        public bool check_if_inserted_in_tbl_sections_history(int section_id, string year, int month)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM statistics_sections_history WHERE section_id=" + section_id + " AND year=" + year + " AND month=" + month;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            bool insert_state = false;
            if(dt.Rows.Count > 0)
            {
                insert_state = true;
            }

            return insert_state;
        }

        //insert to tbl_sections_history
        public void insert_to_sections_history(string year, int month,int section_id,string section_type , string section_name,string send_state,string answer_state,string approve_state)
        {
            try
            {
                string insert = "INSERT INTO statistics_sections_history (appoval_sequnce,id,month,year,section_id,section_type,section_name,send_state,answer_state,approve_state,created_at,updated_at) VALUES(@appoval_sequnce,@id,@month,@year,@section_id,@section_type,@section_name,@send_state,@answer_state,@approve_state,@created_at,@updated_at)";
                SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());
                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("statistics_sections_history");
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_insert.Parameters.Add("@section_type", SqlDbType.VarChar).Value = section_type;
                cmd_insert.Parameters.Add("@section_name", SqlDbType.VarChar).Value = section_name;
                cmd_insert.Parameters.Add("@send_state", SqlDbType.VarChar).Value = send_state;
                cmd_insert.Parameters.Add("@answer_state", SqlDbType.VarChar).Value = answer_state;
                cmd_insert.Parameters.Add("@approve_state", SqlDbType.VarChar).Value = approve_state;
                cmd_insert.Parameters.Add("@created_at", SqlDbType.VarChar).Value = now;
                cmd_insert.Parameters.Add("@updated_at", SqlDbType.VarChar).Value = now;
                cmd_insert.Parameters.Add("@appoval_sequnce", SqlDbType.Int).Value = get_last_approval_sequnce();

                cmd_insert.ExecuteNonQuery();
                //MessageBox.Show("Insert Successfully","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

            
            }catch(Exception e)
            {
                MessageBox.Show("Error in db class in function (insert_to_tbl_sections_history) , Error is: " + e.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void insert_to_tbl_statistics_months_history(int month , string year)
        {
            try
            {
                string query = "INSERT INTO statistics_months_history (id,month,year,noSections,noBranchs,noDepartments) VALUES(@id,@month,@year,@noSections,@noBranchs,@noDepartments)";

                if(check_month_in_statistics_months_history(year,month))
                {
                    query = "UPDATE statistics_months_history SET month=@month,year=@year,noSections=@noSections,noBranchs=@noBranchs,noDepartments=@noDepartments WHERE year=@year AND month=@month";
                }

                int noSections = get_count_section_with_type("branch") + get_count_section_with_type("department");
                SqlCommand cmd_insert = new SqlCommand(query, sqlcon());
                
                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("statistics_months_history");
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@noSections", SqlDbType.Int).Value = noSections;
                cmd_insert.Parameters.Add("@noBranchs", SqlDbType.Int).Value = get_count_section_with_type("branch");
                cmd_insert.Parameters.Add("@noDepartments", SqlDbType.Int).Value = get_count_section_with_type("department");
                cmd_insert.ExecuteNonQuery();
               // MessageBox.Show("Success","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);



            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in function (insert_to_statistics_months_history) , Error is: " + e.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ////////////
        ///             2021/03/20 
        /// ////////
        /// </summary>
        /// <returns></returns>
        public int get_last_approval_sequnce()
        {
            int last_sequnce = 0;
            string query = "SELECT TOP 1 FROM statistics_months_history order by id desc";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach(DataRow row in dt.Rows)
            {
                last_sequnce = Convert.ToInt32(row["appoval_sequnce"].ToString())+1;
            }
            return last_sequnce;
        }
        public int get_count_section_with_type(string type)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_sections WHERE section_type=@type";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt.Rows.Count;
        }
       
        public bool check_month_in_statistics_months_history(string year ,int month)
        {
            string query = "SELECT month FROM statistics_months_history WHERE year=@year AND month=@month";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            DataTable dt = new DataTable();
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            bool is_found = false;
            if(dt.Rows.Count > 0 )
            {
                is_found = true;
            }
            return is_found;
        }
        public void empty(string year , int month)
        {
            string query = "DELETE FROM statistics_sections_history WHERE year="+year+" AND month="+month;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.ExecuteNonQuery();
            
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////END STATISTCIS HISTORY///////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        
        
        
        
        
        
        //////////////////////////////////////     End Questions Methods   //////////////////////////////////////





        //////////////////////////////////////     Start Users Methods   //////////////////////////////////////

        //Get All Users
        public DataTable get_all_users()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_users ORDER BY id ASC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dt.Columns["id"].ColumnName = "ID";
            dt.Columns["UserJobTitle"].ColumnName = "Job Title";
            dt.Columns["UserFullName"].ColumnName = "Name";
            dt.Columns["UserUsername"].ColumnName = "Username";
            dt.Columns["UserPassword"].ColumnName = "Password";
            dt.Columns["created_at"].ColumnName = "Created in";
            dt.Columns["updated_at"].ColumnName = "Last Update";
            dt.Columns["section_id"].ColumnName = "Section ID";
          


            return dt;

        }
        //Get Users For A Section
        public DataTable get_users_for_section(int section_id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_users WHERE section_id=@section_id ORDER BY id ASC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dt.Columns["id"].ColumnName = "ID";
            dt.Columns["UserFullName"].ColumnName = "الاسم";
            dt.Columns["UserUsername"].ColumnName = "اسم المستخدم";
            dt.Columns["UserPassword"].ColumnName = "كلمة السر";
            dt.Columns["UserJobTitle"].ColumnName = "العنوان الوظيفي";
            dt.Columns["phone"].ColumnName = "الهاتف";
            
            dt.Columns.Remove("created_at");
            dt.Columns.Remove("updated_at");
            ///dt.Columns.Remove("section_id");

            Bitmap delete = mybitmap(@"D:\MyApp\Questionnaires\images\delete.png", 27, 24);
           
            DataColumn iamge_column = new DataColumn("حذف");
            iamge_column.DataType = System.Type.GetType("System.Byte[]");
           
            var imageConverter = new ImageConverter();
            var image = imageConverter.ConvertTo(delete, System.Type.GetType("System.Byte[]"));
            iamge_column.DefaultValue = image;
            dt.Columns.Add(iamge_column);
            return dt;

        }
        public Bitmap mybitmap(string path, int h, int w)
        {

            var img = Bitmap.FromFile(path);
            Bitmap resized = new Bitmap(img, new Size(h, w));
            resized.Save("DSC_0000.jpg");
            return resized;
        }
        //Validate User Before Create
        public bool validate_user(string myusername)
        {
            bool valid = false;
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT * FROM tbl_users WHERE UserUsername='" + myusername + "'";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    valid = false;
                }
                else
                {
                    valid = true;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in valid user is : " + e.Message);
            }


            return valid;
        }

        //Create User
        public void create_user(string fullname, string UserJobTitle,string phone, string username, string password, int section_id)
        {
            if (validate_user(username) == true)
            {
                String query = "INSERT INTO tbl_users (id,UserFullName,UserJobTitle,phone,UserUsername,UserPassword,section_id,created_at) Values(@id,@_fullname,@UserJobTitle,@phone,@_username,@_password,@section_id,@created_at)";

                using (SqlCommand cmd = new SqlCommand(query))
                {


                    cmd.Connection = sqlcon();
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("tbl_users");
                    cmd.Parameters.Add("@_fullname", SqlDbType.VarChar, 50).Value = fullname;
                    cmd.Parameters.Add("@_username", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@_password", SqlDbType.VarChar, 200).Value = password;
                    cmd.Parameters.Add("@UserJobTitle", SqlDbType.VarChar, 50).Value = UserJobTitle;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar, 16).Value = phone;
                    cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
  


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Save Successfully", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(
                        "Sorry The Username Is Taken Already , Please Change Another Username !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Edit User
        public void edit_user(int user_id , string fullname,string user_job_title, string username, string password,string phone, int section_id, bool msg, bool same_user)
        {
           // MessageBox.Show(user_id.ToString());
            try
            {
                if (same_user == true)
                {

                    String query = "UPDATE tbl_users SET UserFullName=@_fullname,UserJobTitle=@_user_job_title,phone=@_phone,UserUsername=@_username,UserPassword=@_password,section_id=@_section_id , updated_at=@_updated_at WHERE id=" + user_id;

                    using (SqlCommand cmd = new SqlCommand(query))
                    {


                        cmd.Connection = sqlcon();

                        cmd.Parameters.Add("@_fullname", SqlDbType.VarChar, 50).Value = fullname;
                        cmd.Parameters.Add("@_user_job_title", SqlDbType.VarChar, 50).Value = user_job_title;
                        cmd.Parameters.Add("@_username", SqlDbType.VarChar, 50).Value = username;
                        cmd.Parameters.Add("@_password", SqlDbType.VarChar, 200).Value = password;
                        cmd.Parameters.Add("@_phone", SqlDbType.VarChar, 200).Value = phone;
                        cmd.Parameters.Add("@_section_id", SqlDbType.Int).Value = section_id;
                        
                        cmd.Parameters.Add("@_updated_at", SqlDbType.DateTime, 50).Value = now;


                        cmd.ExecuteNonQuery();
                        if (msg == true)
                        {
                            MessageBox.Show("Save Successfully", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }
                }
                else
                {
                    if (validate_user(username) == true)
                    {
                        String query = "UPDATE tbl_users SET UserFullName=@_fullname,UserJobTitle=@_user_job_title,phone=@_phone,UserUsername=@_username,UserPassword=@_password,section_id=@_section_id , updated_at=@_updated_at WHERE section_id=" + section_id;

                        using (SqlCommand cmd = new SqlCommand(query))
                        {


                            cmd.Connection = sqlcon();

                            cmd.Parameters.Add("@_fullname", SqlDbType.VarChar, 50).Value = fullname;
                            cmd.Parameters.Add("@_user_job_title", SqlDbType.VarChar, 50).Value = user_job_title;
                            cmd.Parameters.Add("@_username", SqlDbType.VarChar, 50).Value = username;
                            cmd.Parameters.Add("@_password", SqlDbType.VarChar, 200).Value = password;
                            cmd.Parameters.Add("@_phone", SqlDbType.VarChar, 200).Value = phone;
                            cmd.Parameters.Add("@_section_id", SqlDbType.Int).Value = section_id;

                            cmd.Parameters.Add("@_updated_at", SqlDbType.DateTime, 50).Value = now;


                            cmd.ExecuteNonQuery();
                            if (msg == true)
                            {
                                MessageBox.Show("Save Successfully", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                                "Sorry The Username Is Taken Already , Please Change Another Username !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in edit user is: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        //Delete User
        public void delete_user(int id)
        {
            try
            {
                string query = "DELETE FROM tbl_users WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete success .", "Done .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch(Exception e)
            {
                MessageBox.Show("Error in db class ->delete user error is : " + e.Message, "Error .", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Search User
        public DataTable search_user(string txt , string col)
        {
          

            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_users WHERE "+col+" LIKE '%"+txt+"%' ORDER BY id ASC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
           

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dt.Columns["id"].ColumnName = "ID";
            dt.Columns["UserJobTitle"].ColumnName = "Job Title";
            dt.Columns["UserFullName"].ColumnName = "Name";
            dt.Columns["UserUsername"].ColumnName = "Username";
            dt.Columns["UserPassword"].ColumnName = "Password";
            dt.Columns["created_at"].ColumnName = "Created in";
            dt.Columns["updated_at"].ColumnName = "Last Update";
            dt.Columns["section_id"].ColumnName = "Section ID";



            return dt;

        }

        //Get Info User To Edit
        public DataTable get_info_user_to_edit(int user_id)
        {
        
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_users WHERE id=@id ORDER BY id ASC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@id",SqlDbType.Int).Value = user_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
      
            return dt;

        
        }

        //Enable / Disable User
        public void active_user(int user_id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_users WHERE id=@id ORDER BY id ASC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            string enable="";
            foreach(DataRow row in dt.Rows)
            {
                enable = row["enable"].ToString();
            }
            if(enable =="0")
            {
                enable = "1";
            }
            else
            {
                enable = "0";
            }
            string query_toggle = "UPDATE tbl_users SET enable=@enable WHERE id=@id";
            SqlCommand cmd_toggle = new SqlCommand(query_toggle, sqlcon());
            cmd_toggle.Parameters.Add("@enable", SqlDbType.Int).Value = Convert.ToInt32(enable);
            cmd_toggle.Parameters.Add("@id", SqlDbType.Int).Value = user_id;
            cmd_toggle.ExecuteNonQuery();
            
        }
        //////////////////////////////////////     End Users Methods   //////////////////////////////////////








        //////////////////////////////////////     Start Answers Methods   //////////////////////////////////////

        //Get Answers A Section
        public DataTable get_answers_section(int section_id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_months WHERE section_id=@section_id";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            /////////////////////////////////////////////////////////
            dt.Columns.Remove("section_id");
            dt.Columns.Remove("updated_at");
            
            dt.Columns.Remove("renable");
            dt.Columns.Remove("history");
            dt.Columns.Remove("id");
            dt.Columns.Remove("date_history");

            dt.Columns.Remove("admin_editor");

            dt.Columns["edit_answer"].ColumnName = "التعديل";
            
            dt.Columns["approve"].ColumnName = "الموافقة";
            //dt.Columns["approve"].SetOrdinal(6);
            dt.Columns["answer_state"].ColumnName = "حالة الاجابة";
            dt.Columns["month"].ColumnName = "شهر";
            dt.Columns["year"].ColumnName = "سنة";
            dt.Columns["created_at"].ColumnName = "تاريخ الارسال";
            dt.Columns["questionnaier_section_no"].ColumnName = "رقم الاستبيان";
            dt.Columns["date_answer"].ColumnName = "تاريخ الاجابة";
            dt.Columns["date_approve"].SetOrdinal(9);
            
            dt.Columns["date_approve"].ColumnName = "تاريخ الموافقة";
            
            dt.Columns["user_answer"].ColumnName = "المستخدم";
            //DataColumn col_view = new DataColumn();
            DataColumn col_count = new DataColumn();

            //mydt.Columns.Add(view_column);
          //  view_column.SetOrdinal(7);
            
           // col_view.ColumnName = "عرض";
            col_count.ColumnName = "عدد الاسئلة";
            
            add_column_icon(dt, 10, "عرض", "view", 90, 50);
            add_column_icon(dt, 11, "حذف", "delete-disable", 40, 40);
            add_column_icon(dt, 12, "اعادة تفعيل", "renable-disable", 40, 40);
            add_column_icon(dt, 13, "الأرشيف", "history3", 60, 60);
            

            dt.Columns.Add(col_count);
            col_count.SetOrdinal(3);
            /////////////////////////////////////////////////////////
            return dt;
        }
        public void add_column_icon(DataTable mydt,int col_index, string col_text , string img_name , int h , int y)
        {
            Bitmap myimage = mybitmap(@"D:\MyApp\Questionnaires\images\"+img_name+".png", h, y);
            DataColumn col = new DataColumn(col_text);
            col.DataType = System.Type.GetType("System.Byte[]");
            var imageConverter = new ImageConverter();
            var image = imageConverter.ConvertTo(myimage, System.Type.GetType("System.Byte[]"));
            col.DefaultValue = image;
            mydt.Columns.Add(col);
            col.SetOrdinal(col_index);
        }
        //Get count question A Section
        public int get_count_questions_section(int section_id)
        {

            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_questions WHERE section_id=@section_id";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt.Rows.Count;
        }

        //Get count Answers A Section
        public int get_count_answers_section(int section_id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_questions_log WHERE section_id=@section_id";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt.Rows.Count;
        }
        
        //Get Column Count Questions
        public int get_column_count_questions(int section_id, string year, int month)
        {
           // MessageBox.Show("section id is : " + section_id + " year : " + year + " month: " + month);
            DataTable dt = new DataTable();
            string query = "SELECT * from tbl_questions_log WHERE section_id="+section_id+" AND year='"+year+"' AND month="+month;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt.Rows.Count;
        }

        //delete exam
        public void delete_exam_from_tbl_months(int questionnaier_section_no,int section_id , string year , int month)
        {
            
            string query = "DELETE FROM tbl_months WHERE questionnaier_section_no=@questionnaier_section_no";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@questionnaier_section_no", SqlDbType.Int).Value = questionnaier_section_no;
            cmd.ExecuteNonQuery();

            string query_statistics_sections_history = "UPDATE statistics_sections_history SET send_state='لم يتم الارسال', answer_state='-', approve_state='-'  WHERE year=@year AND month=@month AND section_id=@section_id";

            cmd = new SqlCommand(query_statistics_sections_history, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
           
            cmd.ExecuteNonQuery();
            //MessageBox.Show("delete success");
        }
        public bool check_approve_month(string year , int month , int section_id)
        {
            //MessageBox.Show("year: " + year +" month: "+" section_id: "+section_id.ToString());
            DataTable dt = new DataTable();
            string query = "SELECT approve FROM tbl_months WHERE section_id=@section_id AND year=@year AND month=@month AND approve='تم الموافقة'";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            bool has_approved = false;
           

            if(dt.Rows.Count > 0)
            {
                has_approved = true;
            }
            return has_approved;
        }
        //get answers log 
        public DataTable get_answers_log_for_section(int section_id , int month)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_questions_log WHERE section_id=@section_id AND month=@month order by id_q desc";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        //Get  approve state
        public DataTable get_approve_state(int section_id,int month)
        {
          
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_months WHERE section_id=@section_id AND month=@month";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            
            return dt;
        }
        //Approve answer
        public void approve_answer(int section_id ,string year, int month ,string approve)
        {
            string text = "";
            string date = "";
            if(approve =="منح الموافقة")
            {
                text = "تم الموافقة";
                date = now;
            }
            else
            {
                text = "لم تتم الموافقة";
                date = "-";
            }
            string query = "UPDATE tbl_months SET approve=@text , date_approve=@date_approve  WHERE year=@year AND month=@month AND section_id=@section_id";
            string query_statistics_sections_history = "UPDATE statistics_sections_history SET approve_state=@text  WHERE year=@year AND month=@month AND section_id=@section_id";

            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@text", SqlDbType.VarChar).Value = text;
            cmd.Parameters.Add("@date_approve", SqlDbType.VarChar).Value = date;
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand(query_statistics_sections_history, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@text", SqlDbType.VarChar).Value = text;
            cmd.Parameters.Add("@date_approve", SqlDbType.VarChar).Value = date;
            cmd.ExecuteNonQuery();

            MessageBox.Show(text);


        }

        //edit assetment
        public void edit_answer(int section_id, int month, int sequnce , int degree)
        {

            string update_log = "UPDATE tbl_questions_log SET updated_at=@updated_at, event_assetment_after_edit=@degree, edit_answer='تم التعديل' WHERE month=@month AND section_id=@section_id and id_q=@sequnce";
            
            SqlCommand cmd_log = new SqlCommand(update_log, sqlcon());
            

            cmd_log.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd_log.Parameters.Add("@month", SqlDbType.Int).Value = month;
            cmd_log.Parameters.Add("@sequnce", SqlDbType.Int).Value = sequnce;
            cmd_log.Parameters.Add("@degree", SqlDbType.Int).Value = degree;
            cmd_log.Parameters.Add("@event_assetment_after_edit", SqlDbType.Int).Value = degree;
            cmd_log.Parameters.Add("@updated_at", SqlDbType.VarChar).Value = now2;

            cmd_log.ExecuteNonQuery();
            //////////////////////////////////////
            if(check_edits(section_id,month))
            {
                string update_months = "UPDATE tbl_months SET edit_answer='تم التعديل'  WHERE month=@month AND section_id=@section_id";
                SqlCommand cmd_months = new SqlCommand(update_months, sqlcon());
                cmd_months.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_months.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_months.ExecuteNonQuery();
            }
            
            MessageBox.Show("Success");


        }

        //reset edits
        public void reset_edits(int section_id , int month , int sequnce)
        {
            try
            {
                string reset = "UPDATE tbl_questions_log SET updated_at ='' ,edit_answer='0' , event_assetment_after_edit='' WHERE section_id=@section_id AND month=@month AND id_q=@sequnce";
                SqlCommand cmd = new SqlCommand(reset, sqlcon());
                cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@sequnce", SqlDbType.Int).Value = sequnce;
                cmd.ExecuteNonQuery();
                ////////////////////
                string update_months = "UPDATE tbl_months SET edit_answer='لايوجد تعديل'  WHERE month=@month AND section_id=@section_id";
                SqlCommand cmd_months = new SqlCommand(update_months, sqlcon());
                cmd_months.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_months.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_months.ExecuteNonQuery();
                MessageBox.Show("تم الارجاع الى الحالة الاصلية بنجاح","تم",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception e)
            {
                MessageBox.Show("Error in db class in reset edits , error is : " + e.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        //Check Edits 
        public bool check_edits(int section_id , int month )
        {
            DataTable dt = new DataTable();
            string query = "SELECT event_assetment_after_edit FROM tbl_questions_log WHERE section_id=@section_id AND month=@month";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            bool has_edit = false;

            foreach(DataRow row in dt.Rows)
            {
                string event_assetment_after_edit = row[0].ToString();
                if(event_assetment_after_edit != "0")
                {
                    has_edit = true;
                }
            }
            return has_edit;
        }


        /////////////////////////////////////////////////////////////////////////////
        ///////////////////////////// HISTORY ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        public string date_answer, user_answer, date_send;
        //Renable

        //[1] edit on tbl_log
        public void renable(int section_id ,string year , int month )
        {
            //MessageBox.Show("section_id: " + section_id + " year: " + year + " month : " + month);
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_questions_log WHERE section_id=@section_id AND year=@year AND month=@month";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                date_answer = row["date_answer"].ToString();
                user_answer = row["user_answer"].ToString();
                date_send = row["date_send"].ToString();
            
                //Questions
                insert_to_history_tbl(row,section_id);
                update_tbl_log_to_history(section_id, year, month, Convert.ToInt32(row["id_q"].ToString()));
            }
            insert_to_tbl_months_history(section_id,year,month);
            update_tl_month_to_history(section_id, year, month);
            //section history tbl
            string query_statistics_sections_history = "UPDATE statistics_sections_history SET send_state='تم الارسال', answer_state='-', approve_state='-'  WHERE year=@year AND month=@month AND section_id=@section_id";
             
            SqlCommand cmd_statistics = new SqlCommand(query_statistics_sections_history, sqlcon());
            cmd_statistics.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
            cmd_statistics.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
            cmd_statistics.Parameters.Add("@month", SqlDbType.Int).Value = month;

            cmd_statistics.ExecuteNonQuery();
            MessageBox.Show("تم اعادة التفعيل بنجاح", "نجح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public void insert_to_tbl_months_history(int section_id,string year , int month)
        {
            try
            {
               // section_id = 3;
              //  MessageBox.Show("section_id: " + section_id + " year: " + year + " month : " + month);
                string insert = "INSERT INTO tbl_months_history (id,section_id,date_archive,user_answer,date_answer,date_send,year,month) VALUES(@id,@section_id,@date_archive,@user_answer,@date_answer,@date_send,@year,@month)";
               // string insert = "INSERT INTO tbl_months_history (id,section_id) VALUES(@id,@section_id)";

                SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());
                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("tbl_months_history");
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_insert.Parameters.Add("@date_archive", SqlDbType.VarChar).Value = now;
                cmd_insert.Parameters.Add("@user_answer", SqlDbType.VarChar).Value = user_answer;
                cmd_insert.Parameters.Add("@date_answer", SqlDbType.VarChar).Value = date_answer;
                cmd_insert.Parameters.Add("@date_send", SqlDbType.VarChar).Value = date_send;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
               
                cmd_insert.ExecuteNonQuery();

               // MessageBox.Show("insert into tbl_months_history success");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in history insert into tbl_months is : " + e.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void insert_to_history_tbl(DataRow row , int section_id)
        {
            try
            {
                string insert = "INSERT INTO tbl_questions_log_history (id,questionnaier_section_no , id_q ,state,date_send,date_archive,q_type,q_text,answer,date_answer,user_answer,history,event_type,event_details,event_impact,event_source,event_assetment,event_notic,section_id,year,month,category_id,id_exam,event_assetment_after_edit,updated_at,id_history) VALUES(@id_log ,@questionnaier_section_no, @id_q ,@state,@date_send,@date_archive,@q_type,@q_text,@answer,@date_answer,@user_answer,@history,@event_type,@event_details,@event_impact,@event_source,@event_assetment,@event_notic,@section_id,@year,@month,@category_id,@id_exam,@event_assetment_after_edit,@updated_at,@id_history)";

                SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());
                cmd_insert.Parameters.Add("@id_log", SqlDbType.Int).Value = get_last_id("tbl_questions_log_history");
                cmd_insert.Parameters.Add("@questionnaier_section_no", SqlDbType.Int).Value = get_last_no_questionnaier_for_section(section_id, true);
                cmd_insert.Parameters.Add("@id_q", SqlDbType.Int).Value = Convert.ToInt32(row["id_q"].ToString());
                cmd_insert.Parameters.Add("@date_send", SqlDbType.VarChar).Value = row["date_send"].ToString();
                cmd_insert.Parameters.Add("@date_archive", SqlDbType.VarChar).Value = now;
                cmd_insert.Parameters.Add("@q_text", SqlDbType.VarChar).Value = row["q_text"].ToString();
                cmd_insert.Parameters.Add("@q_type", SqlDbType.VarChar).Value = row["q_type"].ToString();
                cmd_insert.Parameters.Add("@answer", SqlDbType.Int).Value = Convert.ToInt32(row["answer"].ToString());
                cmd_insert.Parameters.Add("@date_answer", SqlDbType.VarChar).Value = row["date_answer"].ToString();
                cmd_insert.Parameters.Add("@user_answer", SqlDbType.VarChar).Value = row["user_answer"].ToString();
                cmd_insert.Parameters.Add("@history", SqlDbType.Int).Value = 1;
                cmd_insert.Parameters.Add("@event_type", SqlDbType.Text).Value = row["event_type"].ToString();
                cmd_insert.Parameters.Add("@event_details", SqlDbType.Text).Value = row["event_details"].ToString();
                cmd_insert.Parameters.Add("@event_impact", SqlDbType.Text).Value = row["event_impact"].ToString();
                cmd_insert.Parameters.Add("@event_source", SqlDbType.Text).Value = row["event_source"].ToString();
                cmd_insert.Parameters.Add("@event_assetment", SqlDbType.Int).Value = row["event_assetment"].ToString();
                cmd_insert.Parameters.Add("@event_notic", SqlDbType.Text).Value = row["event_notic"].ToString();
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = Convert.ToInt32(row["section_id"].ToString());
                cmd_insert.Parameters.Add("@category_id", SqlDbType.Int).Value = Convert.ToInt32(row["category_id"].ToString());
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar, 50).Value = row["year"].ToString();
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = row["month"].ToString();
                cmd_insert.Parameters.Add("@id_exam", SqlDbType.Int).Value = get_last_id("tbl_months");
                cmd_insert.Parameters.Add("@state", SqlDbType.Int).Value = Convert.ToInt32(row["state"].ToString());
                cmd_insert.Parameters.Add("@event_assetment_after_edit", SqlDbType.Int).Value = Convert.ToInt32(row["event_assetment_after_edit"].ToString());
                cmd_insert.Parameters.Add("@updated_at", SqlDbType.VarChar, 50).Value = row["updated_at"].ToString();
                cmd_insert.Parameters.Add("@id_history", SqlDbType.Int).Value = get_last_id("tbl_months_history");
                cmd_insert.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in send questions to web is : " + e.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void update_tbl_log_to_history(int section_id , string year , int month , int sequnce)
        {
            try
            {
                string insert = "UPDATE  tbl_questions_log SET state=0 ,date_send=@date_send,answer=0,date_answer='',user_answer='',event_type='',event_details='',event_impact='',event_source='',event_assetment=0,event_notic='',event_assetment_after_edit=0,updated_at='' WHERE section_id=@section_id AND year=@year AND month=@month AND id_q=@sequnce";

                SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());

                cmd_insert.Parameters.Add("@date_send", SqlDbType.VarChar).Value = now2;
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_insert.Parameters.Add("@sequnce", SqlDbType.Int).Value = sequnce;

                cmd_insert.ExecuteNonQuery();
               // MessageBox.Show("edit success");

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in history at edit tbl_questions_log is : " + e.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void update_tl_month_to_history(int section_id, string year, int month)
        {
            try
            {
                string insert = "UPDATE  tbl_months SET year=@year,month=@month,created_at=@created_at,answer_state='لم تتم الاجابة',user_answer='-',date_answer='-',approve='لم تتم الموافقة',date_approve='-',renable=1,history=1,admin_editor='-',date_history='-' WHERE section_id=@section_id AND year=@year AND month=@month";

                SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());

                cmd_insert.Parameters.Add("@created_at", SqlDbType.VarChar).Value = now2;
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;

                cmd_insert.ExecuteNonQuery();
               // MessageBox.Show("tbl_months edit success");

            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in history at edit tbl_months is : " + e.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Get Archives
        public DataTable get_archives(int section_id , string year , int month)
        {
            string query = "SELECT * FROM tbl_months_history WHERE section_id="+section_id+" AND year="+year+" AND month="+month;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dt.Columns.Remove("year");
            dt.Columns.Remove("month");
            dt.Columns.Remove("section_id");

            //dt.Columns["date_send"].ColumnName = "تاريخ الارسال";
            //dt.Columns["date_archive"].ColumnName = "تاريخ الارشفة";
            //dt.Columns["date_answer"].ColumnName = "تاريخ الاجابة";
            //dt.Columns["user_answer"].ColumnName = "اسم المستخدم";
            
            return dt;
        }

        //Get Questions From Archive
        public DataTable get_questions_from_archive(int section_id , string year , int month, int id_history)
        {
            string query = "SELECT * FROM tbl_questions_log_history WHERE section_id=" + section_id + " AND year=" + year + " AND month=" + month + " AND id_history="+id_history+" order by id desc";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;

        }



        public bool check_if_inserted_in_tbl_statistics(int section_id, string year, int month,int category_id, int avarge)
        {
            bool has_inserted = false;
            string verified = "SELECT id FROM tbl_statistics WHERE section_id=" + section_id + " AND year="+year + " AND month=" + month;
            SqlCommand cmd_verified = new SqlCommand(verified, sqlcon());
            DataTable dt =new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd_verified);
            adapter.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                string update = "UPDATE tbl_statistics SET cat_avarge=@cat_avarge WHERE section_id=@section_id AND year=@year AND month=@month AND category_id=@category_id";

                if(category_id ==0)
                {
                     update = "UPDATE tbl_total_statistics SET total=@total WHERE section_id=@section_id AND year=@year AND month=@month";

                }
                SqlCommand cmd_update = new SqlCommand(update, sqlcon());
                cmd_update.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("tbl_statistics");
                cmd_update.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_update.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_update.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_update.Parameters.Add("@cat_avarge", SqlDbType.Int).Value = avarge;
                cmd_update.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;
                cmd_update.Parameters.Add("@total", SqlDbType.Int).Value = avarge;

                cmd_update.ExecuteNonQuery();
                has_inserted = true;
            }
            else
            {
                string insert = "INSERT INTO tbl_statistics (id,section_id,year,month,cat_avarge) VALUES (@id,@section_id,@year,@month,@cat_avarge)";

                if (category_id == 0)
                {
                     insert = "INSERT INTO tbl_total_statistics (id,section_id,year,month,total) VALUES (@id,@section_id,@year,@month,@total)";

                }
                SqlCommand cmd_insert = new SqlCommand(insert, sqlcon());
                cmd_insert.Parameters.Add("@id", SqlDbType.Int).Value = get_last_id("tbl_statistics");
                cmd_insert.Parameters.Add("@section_id", SqlDbType.Int).Value = section_id;
                cmd_insert.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                cmd_insert.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd_insert.Parameters.Add("@total", SqlDbType.Int).Value = avarge;
                cmd_insert.ExecuteNonQuery();

            }
            return has_inserted;

        }
    }
    //public DataTable Avarge()
    //{
    //    DataTable dt = new DataTable();
    //    db db = new cs.db();
    //    string query = "SELECT month,event_assetment FROM tbl_questions_log WHERE event_assetment > 0";
    //    SqlCommand cmd = new SqlCommand(query, db.sqlcon());
    //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //    adapter.FillSchema(dt, SchemaType.Source);
    //    dt.Columns[1].DataType = typeof(Int32);
    //    adapter.Fill(dt);
    //    return dt;
    //}

}
