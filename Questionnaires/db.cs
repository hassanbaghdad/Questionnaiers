using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Questionnaires
{
    class db
    {
        //Public Variables
        DateTime now = DateTime.Now;


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


        //////////////////////////////////////     Departments Methods   //////////////////////////////////////
        //Get All Departments
        public DataTable get_departments()
        {
            DataTable dt = new DataTable();
            String query = "SELECT * FROM tbl_departments";
            SqlCommand cmd = new SqlCommand(query,sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
           
            adapter.Fill(dt);
            
            adapter.Dispose();

            return dt;
        }
      
        //Create Department
        public void create_department(String de_name ,bool msg)
        {
            try
            {
                String query = "INSERT INTO tbl_departments (department_name,enable,created_at,updated_at) Values(@departname,1,@created_at,@updated_at)";

                using (SqlCommand cmd = new SqlCommand(query))
                {

                    
                    cmd.Connection = sqlcon();
                    cmd.Parameters.Add("@departname", SqlDbType.VarChar, 50).Value = de_name;
                    
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
                    cmd.Parameters.Add("@updated_at", SqlDbType.DateTime, 50).Value = now;


                    cmd.ExecuteNonQuery();
                    if( msg ==true)
                    {
                        MessageBox.Show("Save Success", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }catch(Exception e)
            {
                MessageBox.Show("Error in db class in create department is : "+e.Message);
            }
           
            

        }

        //Delete Department
        public void delete_dpartment(int id_depart)
        {
            try
            {
                String query = "Delete From tbl_departments Where id =" + id_depart;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
            }catch(Exception e)
            {
                MessageBox.Show("Error in db class in edit department is : " + e.Message);
            }
            

        }
        
        //Edit Deprtment
        public void edit_department(int id_depart , String depart_name , int enable)
        {
            try
            {
                String query = "Update tbl_departments SET department_name=@depart_name , enable=@enable WHERE id=@id";

                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.Parameters.AddWithValue("@depart_name", depart_name);
                cmd.Parameters.AddWithValue("@enable", enable);
                cmd.Parameters.AddWithValue("@id", id_depart);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Edit Success", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch(Exception e)
            {
                MessageBox.Show("Error in db class in edit department is : " + e.Message);
            }
            
           
        }

        //Get the Department To Edit
        public DataTable get_deprt_to_edit(int id)
        {
                DataTable dt = new DataTable();
                String query = "SELECT * FROM tbl_departments WHERE id=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            
           
            
        }

        //Delete All Questions for Department
        public void delete_all_question_for_department(int id)
        {
            try
            {
                string query = "DELETE FROM tbl_questions WHERE depart_id=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Success");
            }catch(Exception e)
            {
                MessageBox.Show("Error in db class in delete all questions for department is : " + e.Message);
            }
        }

        //Get Last ID
        public int last_id_department()
        {
            DataTable dt = new DataTable();
            int index = 0;
            try
            {
                

                string query = "SELECT TOP 1 * FROM tbl_departments ORDER BY id DESC";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                
                foreach (DataRow row in dt.Rows)
                {
                    index = Convert.ToInt32(row[0].ToString());
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error in db class in get last ID for department is : " + e.Message);
            }
           
            
           
            
            return index;
        }

        //Push A Department
        public void push_department(int depart_id)
        {
            try
            {
                string query = "UPDATE tbl_departments SET enable=1 WHERE id=" + depart_id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Pushed .", "Pushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in Push department is : " + e.Message);
            }

        }
        //UnPush A Department
        public void unpush_department(int depart_id)
        {
            try
            {
                string query = "UPDATE tbl_departments SET enable=0 WHERE id=" + depart_id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("UnPushed .", "UnPushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in UnPush department is : " + e.Message);
            }

        }

        //Push All Questionnaires to All Departments
        public void push_all_questionnaires()
        {
            try
            {
                string query = "UPDATE tbl_departments SET enable=1";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("All Pushed .", "Pushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in Push All departments is : " + e.Message);
            }
           
        }

        //Unpush All Questionnaires to All Departments
        public void unpush_all_questionnarires()
        {
            try
            {
                string query = "UPDATE tbl_departments SET enable=0";
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
                MessageBox.Show("All UnPushed .", "UnPushed .", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in UnPush All departments is : " + e.Message);

            }
            
        }
        //////////////////////////////////////     End Departments Methods   //////////////////////////////////////







        //////////////////////////////////////     Start Questions Methods ////////////////////////////////////////

        //Get All Questions For A Department
        public DataTable get_questions_depart(int id_depart)
        {
           
                DataTable dt = new DataTable();
                String query = "SELECT * FROM tbl_questions WHERE depart_id="+id_depart;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(dt);

                adapter.Dispose();

                return dt;  
            
        }

        //Get Count Questions For A Department
        //public int get_count_questions_depart(int id_depart)
        //{

        //    DataTable dt = new DataTable();
        //    String query = "SELECT id tbl_questions WHERE depart_id=" + id_depart;
        //    SqlCommand cmd = new SqlCommand(query, sqlcon());
        //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        //    adapter.Fill(dt);

        //    adapter.Dispose();
           
        //    return dt.Rows.Count;

        //}

        //Create Question For Department
        public void create_question_for_department( string text ,string notic,int id_depart )
        {
            try
            {
                String query = "INSERT INTO tbl_questions (q_text,notic,depart_id,created_at,updated_at) Values(@q_text,@notic,@id_depart,@created_at,@updated_at)";

                using (SqlCommand cmd = new SqlCommand(query))
                {


                    cmd.Connection = sqlcon();
                   
                    cmd.Parameters.Add("@q_text", SqlDbType.Text).Value = text;
                    cmd.Parameters.Add("@id_depart", SqlDbType.Int, 50).Value = id_depart;
                    cmd.Parameters.Add("@notic", SqlDbType.Text).Value = notic;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
                    cmd.Parameters.Add("@updated_at", SqlDbType.DateTime, 50).Value = now;


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Save Successfully","OK",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in db class in create question for department is : " + e.Message);
            }
        }


        //Get All Departments Names To Select One Whene Add Question
        public DataTable get_departs_name()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_departments";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        //Delelte Question 
        public void delete_question(int id)
        {
            try
            {
                string query = "DELETE FROM tbl_questions where id=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlcon());
                cmd.ExecuteNonQuery();
               
            }catch(Exception e)
            {
                MessageBox.Show("Error in db class in create question for department is : " + e.Message);
            }
            
        }

        //////////////////////////////////////     End Questions Methods   //////////////////////////////////////






        //////////////////////////////////////     Start Users Methods   //////////////////////////////////////

        //Get All Users
        public DataTable get_all_users()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_users";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dt.Columns["id"].ColumnName = "ID";
            dt.Columns["user_fullname"].ColumnName = "Name";
            dt.Columns["user_username"].ColumnName = "Username";
            dt.Columns["user_password"].ColumnName = "Password";
            dt.Columns["created_at"].ColumnName = "Created in";
            dt.Columns["updated_at"].ColumnName = "Last Update";
            dt.Columns["depart_id"].ColumnName = "Depart ID";
            dt.Columns["user_rank"].ColumnName = "Rank";


            return dt;

        }

        //Validate User Before Create
        public bool validate_user(string myusername)
        {
            bool valid = false;
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT * FROM tbl_users WHERE user_username='"+ myusername+"'" ;
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
        public void create_user(string fullname , string username , string password , int depart_id)
        {
            if(validate_user(username)==true)
            {
                String query = "INSERT INTO tbl_users (user_fullname,user_username,user_password,depart_id) Values(@_fullname,@_username,@_password,@_depart_id)";

                using (SqlCommand cmd = new SqlCommand(query))
                {


                    cmd.Connection = sqlcon();

                    cmd.Parameters.Add("@_fullname", SqlDbType.VarChar,50).Value = fullname;
                    cmd.Parameters.Add("@_username", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@_password", SqlDbType.VarChar, 200).Value = password;
                
                    cmd.Parameters.Add("@_depart_id", SqlDbType.Int).Value = depart_id;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
                    cmd.Parameters.Add("@updated_at", SqlDbType.DateTime, 50).Value = now;


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Save Successfully", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }else
            {
                MessageBox.Show(
                        "Sorry The Username Is Taken Already , Please Change Another Username !","Error", MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
        }

        //Edit User
        public void edit_user(string fullname , string username , string password ,int rank ,int depart_id ,bool msg,bool same_user)
        {
            try
            {
                if (same_user == true)
                {
                    
                    String query = "UPDATE tbl_users SET user_fullname=@_fullname,user_username=@_username,user_password=@_password,user_rank=@_rank,depart_id=@_depart_id WHERE depart_id="+depart_id;

                    using (SqlCommand cmd = new SqlCommand(query))
                    {


                        cmd.Connection = sqlcon();

                        cmd.Parameters.Add("@_fullname", SqlDbType.VarChar, 50).Value = fullname;
                        cmd.Parameters.Add("@_username", SqlDbType.VarChar, 50).Value = username;
                        cmd.Parameters.Add("@_password", SqlDbType.VarChar, 200).Value = password;
                        cmd.Parameters.Add("@_rank", SqlDbType.Int).Value = rank;
                        cmd.Parameters.Add("@_depart_id", SqlDbType.Int).Value = depart_id;
                        cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
                        cmd.Parameters.Add("@updated_at", SqlDbType.DateTime, 50).Value = now;


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
                        String query = "UPDATE tbl_users SET user_fullname=@_fullname,user_username=@_username,user_password=@_password,user_rank=@_rank,depart_id=@_depart_id WHERE depart_id="+depart_id;

                        using (SqlCommand cmd = new SqlCommand(query))
                        {


                            cmd.Connection = sqlcon();

                            cmd.Parameters.Add("@_fullname", SqlDbType.VarChar, 50).Value = fullname;
                            cmd.Parameters.Add("@_username", SqlDbType.VarChar, 50).Value = username;
                            cmd.Parameters.Add("@_password", SqlDbType.VarChar, 200).Value = password;
                            cmd.Parameters.Add("@_rank", SqlDbType.Int).Value = rank;
                            cmd.Parameters.Add("@_depart_id", SqlDbType.Int).Value = depart_id;
                            cmd.Parameters.Add("@created_at", SqlDbType.DateTime, 50).Value = now;
                            cmd.Parameters.Add("@updated_at", SqlDbType.DateTime, 50).Value = now;


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
            }catch(Exception e)
            {
                MessageBox.Show("Error in edit user is: "+e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        //Get User To Edit
        public DataTable get_user_to_edit(int depart_id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tbl_users WHERE depart_id="+ depart_id +" ORDER BY id DESC";
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        //////////////////////////////////////     End Users Methods   //////////////////////////////////////








        //////////////////////////////////////     Start Answers Methods   //////////////////////////////////////
        
        //Get Answers A Departments
        public SqlDataReader get_answers_department(int depart_id)
        {
            
            string query = "SELECT * FROM tbl_departments WHERE id=" + depart_id;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataReader reader = cmd.ExecuteReader();
            
            return reader;
        }

        //Get Count Questions For A department
        public int get_count_questons_department(int depart_id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT id FROM tbl_questions WHERE depart_id=" + depart_id;
            SqlCommand cmd = new SqlCommand(query, sqlcon());
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt.Rows.Count;
        }




    }
}
