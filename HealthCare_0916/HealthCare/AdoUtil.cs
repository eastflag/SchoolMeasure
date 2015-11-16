using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HealthCare
{
	public class AdoUtil
	{
		private string _connectionString = "server=aurasystem.kr;port=3306;uid=healthcare;pwd=!healthcare;Persist Security Info=True;database=healthcare";
		private frmMain _mainForm = null;

		public AdoUtil(frmMain mainForm)
		{
			this._mainForm = mainForm;
		}

		/// <summary>
		/// 로그인 정보 조회
		/// </summary>
		/// <param name="id"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public DataTable SelectLoginInfo(string id, string password)
		{
			MySqlConnection conn = new MySqlConnection();
			MySqlCommand cmd = new MySqlCommand();

			conn.ConnectionString = this._connectionString;
			
			string query = @"	select id, status, role_id
								from manager
								WHERE id = HEX(AES_ENCRYPT(@admin_id, 'aura')) and pass = password(@admin_Pass)";

			try
			{
				conn.Open();
				cmd.Connection = conn;

				cmd.CommandText = query;
				cmd.Parameters.AddWithValue("@admin_Id", id);
				cmd.Parameters.AddWithValue("@admin_Pass", password);

				MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(cmd);

				DataTable dt = new DataTable();
				sqlAdapter.Fill(dt);

				return dt;
			}
			catch (Exception e)
			{
				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
			}
			finally
			{
				conn.Close();
			}

			return null;
		}

		/// <summary>
		/// 학교 정보 조회
		/// </summary>
		/// <param name="search_school"></param>
		/// <returns></returns>
		public DataTable SelectSchoolInfo(string school_name)
		{
			MySqlConnection conn = new MySqlConnection();
			MySqlCommand cmd = new MySqlCommand();

			conn.ConnectionString = this._connectionString;
			
			string query = @"	select school_id, school_name, address 
								from school 
								where school_id in 
								(	
									select member.school_id 
									from member inner join pay_info on member.member_id = pay_info.member_id 
									where member.school_id is not null 
									and (curdate() < date_add(pay_info.pay_date, interval 1 month))
									group by pay_info.member_id, member.school_id
								)
							";


			if (! String.IsNullOrWhiteSpace(school_name))
			{
				query += " and school_name like '%" + @school_name + "%'";
			}

			query += " order by school_name";

			try
			{
				conn.Open();
				cmd.Connection = conn;

				cmd.CommandText = query;

				MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(cmd);

				DataTable dt = new DataTable();
				sqlAdapter.Fill(dt);

				return dt;
			}
			catch (Exception e)
			{
				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
			}
			finally
			{
				conn.Close();
			}

			return null;
		}

		/// <summary>
		/// 학교 정보 조회
		/// </summary>
		/// <param name="school_id"></param>
		/// <returns></returns>
		public DataTable SelectSchoolMemberInfo(int school_id, string grade, string ban, string name)
		{
			MySqlConnection conn = new MySqlConnection();
			MySqlCommand cmd = new MySqlCommand();

			conn.ConnectionString = this._connectionString;

			string query = @"	select 
									member.member_id, 
									CAST(AES_DECRYPT(unhex(member.name), 'aura') AS CHAR(100)) as 'name', 
									member.school_grade, 
									member.school_class, member.sex
, 
									DATE_FORMAT(CAST(AES_DECRYPT(unhex(member.birth_date), 'aura') AS CHAR(100)), '%Y%m%d') AS birth_date, 
									DATE_FORMAT(FROM_DAYS(TO_DAYS(now())-TO_DAYS(CAST(AES_DECRYPT(unhex(member.birth_date), 'aura') AS CHAR(100)))), '%Y') + 0 AS age, 
									member.school_id, school.gubun2        
								
									from member 
								
									inner join school
								
									on member.school_id = school.school_id
								
									inner join pay_info 
									on member.member_id = pay_info.member_id 
									where member.school_id = @school_id and (curdate() < date_add(pay_info.pay_date, interval 1 month))";

			try
			{
				conn.Open();
				cmd.Connection = conn;

				if (! String.IsNullOrWhiteSpace(grade) && grade != "전체")
				{
					query += " and member.school_grade = @grade ";
				}

				if (!String.IsNullOrWhiteSpace(ban) && ban != "전체")
				{
					query += " and member.school_class = @ban ";
				}

				if (!String.IsNullOrWhiteSpace(name))
				{
					query += " and CAST(AES_DECRYPT(unhex(member.name), 'aura') AS CHAR(100)) = @name ";
				}
				
				cmd.CommandText = query;
				cmd.Parameters.AddWithValue("@school_id", school_id);

				if (!String.IsNullOrWhiteSpace(grade) && grade != "전체")
				{
					cmd.Parameters.AddWithValue("@grade", grade);
				}

				if (!String.IsNullOrWhiteSpace(ban) && grade != "ban")
				{
					cmd.Parameters.AddWithValue("@ban", ban);
				}

				if (!String.IsNullOrWhiteSpace(name))
				{
					cmd.Parameters.AddWithValue("@name", name);
				}

				MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(cmd);

				DataTable dt = new DataTable();
				sqlAdapter.Fill(dt);

				return dt;
			}
			catch (Exception e)
			{
				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
			}
			finally
			{
				conn.Close();
			}

			return null;
		}

		/// <summary>
		/// 학생 측정 이력 정보 조회
		/// </summary>
		/// <param name="member_id"></param>
		/// <returns></returns>
		public DataTable SelectSchoolMemberHistoryInfo(int member_id)
		{
			MySqlConnection conn = new MySqlConnection();
			MySqlCommand cmd = new MySqlCommand();

			conn.ConnectionString = this._connectionString;

			string query = @"	select measure_info.measure_date, 
								CAST(AES_DECRYPT(unhex(member.name), 'aura') AS CHAR(100)) as 'name', 
								school.school_name, measure_info.school_grade, measure_info.school_ban
								, inbody_info.Height, inbody_info.Weight, inbody_info.BMI
								, smoke_info.PPM, smoke_info.COHD
								, measure_info.measure_id
 								from measure_info 
								inner join member on measure_info.member_id = member.member_id
								inner join school on measure_info.school_id = school.school_id
 								left outer join inbody_info on measure_info.inbody_seq = inbody_info.inbody_seq
								left join smoke_info on measure_info.smoke_seq = smoke_info.smoke_seq
								where measure_info.member_id = @member_id
								order by measure_date desc, measure_id desc";

			try
			{
				conn.Open();
				cmd.Connection = conn;

				cmd.CommandText = query;
				cmd.Parameters.AddWithValue("@member_id", member_id);

				MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(cmd);

				DataTable dt = new DataTable();
				sqlAdapter.Fill(dt);

				return dt;
			}
			catch (Exception e)
			{
				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
			}
			finally
			{
				conn.Close();
			}

			return null;
		}

		/// <summary>
		/// 측정 기록 삭제
		/// </summary>
		/// <param name="measure_id"></param>
		/// <returns></returns>
		public bool DeleteMeasureInfo(string measure_id)
		{
			bool result = true;

			MySqlConnection conn = new MySqlConnection();
			conn.ConnectionString = this._connectionString;

			MySqlCommand cmd = new MySqlCommand();

			string query = @"	DELETE FROM MEASURE_INFO WHERE measure_id = @measure_id";

			try
			{
				conn.Open();
				cmd.Connection = conn;

				cmd.CommandText = query;
				cmd.Parameters.AddWithValue("@measure_id", measure_id);

				cmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
			}
			finally
			{
				conn.Close();
			}

			return result;
		}

		public bool InsertMeasureInfo(VoStudent student, VoInbody inbodyInfo, VoSmokeInfo smokeInfo)
		{
			bool result = true;

			MySqlConnection conn = new MySqlConnection();
			conn.ConnectionString = this._connectionString;

			MySqlCommand cmd = new MySqlCommand();
			MySqlTransaction tx = null;
			MySqlDataAdapter sqlAdapter = null;

			string querySelectInbodySeq = "select ifnull(max(Inbody_seq + 1), 0) as inbody_seq from inbody_info";
			string querySelectSmokeSeq = "select ifnull(max(smoke_seq + 1), 0) as smoke_seq from smoke_info";
			
			string queryInsertInbodyInfo =
				"INSERT INTO INBODY_INFO(INBODY_SEQ, DATETIMES, Weight, Protein_Mass, Mineral_Mass, Body_Fat_Mass" +
				", Total_Body_Water, Soft_Lean_Mass, Fat_Free_Mass, OSSEOUS, Skeletal_Muscle_Mass, BMI" +
				", Percent_Body_Fat, Waist_Hip_Ratio, Target_Weight,Weight_Control, Fat_Control, Muscle_Control" +
				", Fitness_Score, BMR, Neck, Chest, ABD, THIGHL, ACL, HIP, THIGHR, ACR, Protein_Max, Protein_Min" +
				", Mineral_Max, Mineral_Min, Body_Fat_Mass_Max, Body_Fat_Mass_Min, Weight_Max, Weight_Min" +
				", Skeletal_Muscle_Mass_Max, Skeletal_Muscle_Mass_Min, BMI_Max, BMI_Min, Percent_Body_Fat_Max" +
				", Percent_Body_Fat_Min, Waist_Hip_Ratio_Max, Waist_Hip_Ratio_Min, BMR_MAX, BMR_MIN, FCHEST" +
				", FABD, FACR, FACL, FTHIGHR, FTHIGHL, HEIGHT)" +
				"VALUES(@inbody_seq, @datetimes, @weight, @protein_Mass, @mineral_Mass, @body_Fat_Mass, @total_Body_Water" +
				", @soft_Lean_Mass, @fat_Free_Mass, @oSSEOUS, @skeletal_Muscle_Mass, @bMI, @percent_Body_Fat, @waist_Hip_Ratio" +
				", @target_Weight, @weight_Control, @fat_Control, @muscle_Control, @fitness_Score, @bMR, @neck, @chest, @aBD, @tHIGHL" +
				", @aCL, @hIP, @tHIGHR, @aCR, @protein_Max, @protein_Min, @mineral_Max, @mineral_Min, @body_Fat_Mass_Max, @body_Fat_Mass_Min" +
				", @weight_Max, @weight_Min, @skeletal_Muscle_Mass_Max, @skeletal_Muscle_Mass_Min, @bMI_Max, @bMI_Min, @percent_Body_Fat_Max" +
				", @percent_Body_Fat_Min, @waist_Hip_Ratio_Max, @waist_Hip_Ratio_Min, @bMR_MAX, @bMR_MIN, @fCHEST, @fABD, @fACR, @fACL, @fTHIGHR" +
				", @fTHIGHL, @hEIGHT)";

			string queryInsertSmokeInfo = 
							"INSERT INTO SMOKE_INFO (SMOKE_SEQ, PPM, COHD, DATETIMES) " +
							"VALUES(@smoke_seq, @ppm, @cohd, now())";

			string queryInsertMeasureInfo = @"	INSERT INTO MEASURE_INFO (member_id, school_id, smoke_seq, inbody_seq, school_grade, school_grade_id, school_ban, measure_date, created_date)
												VALUES (@member_id, @school_id, @smoke_seq, @inbody_seq, @school_grade, @school_grade_id, @school_ban, @measure_date, now())";
			string queryUpdateMeasureInfo = @"	UPDATE MEASURE_INFO SET smoke_seq = @smoke_seq, inbody_seq = @inbody_seq, measure_date = @measure_date, created_date = now()
												WHERE measure_id = @measure_id";

			try
			{
				conn.Open();
				cmd.Connection = conn;
				tx = conn.BeginTransaction();

				if (result)
				{
					cmd.CommandText = querySelectInbodySeq;

					sqlAdapter = new MySqlDataAdapter(cmd.CommandText, conn);

					DataTable dt = new DataTable();
					sqlAdapter.Fill(dt);

					if (dt.Rows.Count > 0)
					{
						cmd.CommandText = queryInsertInbodyInfo;
						cmd.Parameters.Clear();

						// Inbody 데이터 저장
						inbodyInfo.Inbody_seq = dt.Rows[0][0].ToString();

						cmd.Parameters.AddWithValue("@inbody_seq", inbodyInfo.Inbody_seq);
						cmd.Parameters.AddWithValue("@datetimes", inbodyInfo.DateTimes);
						cmd.Parameters.AddWithValue("@weight", inbodyInfo.Weight);
						cmd.Parameters.AddWithValue("@protein_Mass", inbodyInfo.Protein_Mass);
						cmd.Parameters.AddWithValue("@mineral_Mass", inbodyInfo.Mineral_Mass);
						cmd.Parameters.AddWithValue("@body_Fat_Mass", inbodyInfo.Body_Fat_Mass);
						cmd.Parameters.AddWithValue("@total_Body_Water", inbodyInfo.Total_Body_Water);
						cmd.Parameters.AddWithValue("@soft_Lean_Mass", inbodyInfo.Soft_Lean_Mass);
						cmd.Parameters.AddWithValue("@fat_Free_Mass", inbodyInfo.Fat_Free_Mass);
						cmd.Parameters.AddWithValue("@oSSEOUS", inbodyInfo.OSSEOUS);
						cmd.Parameters.AddWithValue("@skeletal_Muscle_Mass", inbodyInfo.Skeletal_Muscle_Mass);
						cmd.Parameters.AddWithValue("@bMI", inbodyInfo.BMI);
						cmd.Parameters.AddWithValue("@percent_Body_Fat", inbodyInfo.Percent_Body_Fat);
						cmd.Parameters.AddWithValue("@waist_Hip_Ratio", inbodyInfo.Waist_Hip_Ratio);
						cmd.Parameters.AddWithValue("@target_Weight", inbodyInfo.Target_Weight);
						cmd.Parameters.AddWithValue("@weight_Control", inbodyInfo.Weight_Control);
						cmd.Parameters.AddWithValue("@fat_Control", inbodyInfo.Fat_Control);
						cmd.Parameters.AddWithValue("@muscle_Control", inbodyInfo.Muscle_Control);
						cmd.Parameters.AddWithValue("@fitness_Score", inbodyInfo.Fitness_Score);
						cmd.Parameters.AddWithValue("@bMR", inbodyInfo.BMR);
						cmd.Parameters.AddWithValue("@neck", inbodyInfo.Neck);
						cmd.Parameters.AddWithValue("@chest", inbodyInfo.Chest);
						cmd.Parameters.AddWithValue("@aBD", inbodyInfo.ABD);
						cmd.Parameters.AddWithValue("@tHIGHL", inbodyInfo.THIGHL);
						cmd.Parameters.AddWithValue("@aCL", inbodyInfo.ACL);
						cmd.Parameters.AddWithValue("@hIP", inbodyInfo.HIP);
						cmd.Parameters.AddWithValue("@tHIGHR", inbodyInfo.THIGHR);
						cmd.Parameters.AddWithValue("@aCR", inbodyInfo.ACR);
						cmd.Parameters.AddWithValue("@protein_Max", inbodyInfo.Protein_Max);
						cmd.Parameters.AddWithValue("@protein_Min", inbodyInfo.Protein_Min);
						cmd.Parameters.AddWithValue("@mineral_Max", inbodyInfo.Mineral_Max);
						cmd.Parameters.AddWithValue("@mineral_Min", inbodyInfo.Mineral_Min);
						cmd.Parameters.AddWithValue("@body_Fat_Mass_Max", inbodyInfo.Body_Fat_Mass_Max);
						cmd.Parameters.AddWithValue("@body_Fat_Mass_Min", inbodyInfo.Body_Fat_Mass_Min);
						cmd.Parameters.AddWithValue("@weight_Max", inbodyInfo.Weight_Max);
						cmd.Parameters.AddWithValue("@weight_Min", inbodyInfo.Weight_Min);
						cmd.Parameters.AddWithValue("@skeletal_Muscle_Mass_Max", inbodyInfo.Skeletal_Muscle_Mass_Max);
						cmd.Parameters.AddWithValue("@skeletal_Muscle_Mass_Min", inbodyInfo.Skeletal_Muscle_Mass_Min);
						cmd.Parameters.AddWithValue("@bMI_Max", inbodyInfo.BMI_Max);
						cmd.Parameters.AddWithValue("@bMI_Min", inbodyInfo.BMI_Min);
						cmd.Parameters.AddWithValue("@percent_Body_Fat_Max", inbodyInfo.Percent_Body_Fat_Max);
						cmd.Parameters.AddWithValue("@percent_Body_Fat_Min", inbodyInfo.Percent_Body_Fat_Min);
						cmd.Parameters.AddWithValue("@waist_Hip_Ratio_Max", inbodyInfo.Waist_Hip_Ratio_Max);
						cmd.Parameters.AddWithValue("@waist_Hip_Ratio_Min", inbodyInfo.Waist_Hip_Ratio_Min);
						cmd.Parameters.AddWithValue("@bMR_MAX", inbodyInfo.BMR_MAX);
						cmd.Parameters.AddWithValue("@bMR_MIN", inbodyInfo.BMR_MIN);
						cmd.Parameters.AddWithValue("@fCHEST", inbodyInfo.FCHEST);
						cmd.Parameters.AddWithValue("@fABD", inbodyInfo.FABD);
						cmd.Parameters.AddWithValue("@fACR", inbodyInfo.FACR);
						cmd.Parameters.AddWithValue("@fACL", inbodyInfo.FACL);
						cmd.Parameters.AddWithValue("@fTHIGHR", inbodyInfo.FTHIGHR);
						cmd.Parameters.AddWithValue("@fTHIGHL", inbodyInfo.FTHIGHL);
						cmd.Parameters.AddWithValue("@hEIGHT", inbodyInfo.HEIGHT);

						cmd.ExecuteNonQuery();
					}
					else
					{
						result = false;
					}
				}

				// 흡연 정보가 있을 경우
				if (result && ! String.IsNullOrWhiteSpace(smokeInfo.COHD))
				{
					// Smoke 데이터 저장
					cmd.CommandText = querySelectSmokeSeq;

					sqlAdapter = new MySqlDataAdapter(cmd.CommandText, conn);

					DataTable dt = new DataTable();
					sqlAdapter.Fill(dt);

					if (dt.Rows.Count > 0)
					{
						cmd.CommandText = queryInsertSmokeInfo;
						cmd.Parameters.Clear();

						// Smoke 데이터 저장
						smokeInfo.SmokeSeq = dt.Rows[0][0].ToString();

						cmd.Parameters.AddWithValue("@smoke_seq", smokeInfo.SmokeSeq);
						cmd.Parameters.AddWithValue("@ppm", smokeInfo.PPM);
						cmd.Parameters.AddWithValue("@cohd", smokeInfo.COHD);

						cmd.ExecuteNonQuery();
					}
					else
					{
						result = false;
					}
				}

				if (result)
				{
					// 측정 정보 기록
					bool isUpdateMeasureInfo = false;

					// 이전 측정 정보 기록이 있을 때
					if (! String.IsNullOrWhiteSpace(student.LastMeasureDate))
					{
						DateTime now_date = DateTime.Now;
						DateTime last_measure_date = DateTime.Parse(student.LastMeasureDate);

						// 이전 측정 정보 기록일이 현재의 월과 같은 경우에는 업데이트
						if (now_date.Year == last_measure_date.Year && now_date.Month == last_measure_date.Month)
						{
							isUpdateMeasureInfo = true;
						}
					}

					if (result)
					{
						// UPDATE
						if (isUpdateMeasureInfo)
						{
							cmd.CommandText = queryUpdateMeasureInfo;
							cmd.Parameters.Clear();

							cmd.Parameters.Add("@smoke_seq", MySqlDbType.Int32);

							if (!String.IsNullOrWhiteSpace(smokeInfo.SmokeSeq))
							{
								cmd.Parameters["@smoke_seq"].Value = smokeInfo.SmokeSeq;
							}
							else
							{
								cmd.Parameters["@smoke_seq"].Value = DBNull.Value;
							}

							cmd.Parameters.AddWithValue("@inbody_seq", inbodyInfo.Inbody_seq);
							cmd.Parameters.AddWithValue("@measure_date", student.Measure_Date);
							cmd.Parameters.AddWithValue("@measure_id", student.UpdateMeasureSeq);
						}
						else
						{
							cmd.CommandText = queryInsertMeasureInfo;
							cmd.Parameters.Clear();

							cmd.Parameters.AddWithValue("@member_id", student.ID);
							cmd.Parameters.AddWithValue("@school_id", student.SchoolID);

							cmd.Parameters.Add("@smoke_seq", MySqlDbType.Int32);

							if (!String.IsNullOrWhiteSpace(smokeInfo.SmokeSeq))
							{
								cmd.Parameters["@smoke_seq"].Value = smokeInfo.SmokeSeq;
							}
							else
							{
								cmd.Parameters["@smoke_seq"].Value = DBNull.Value;
							}

							cmd.Parameters.AddWithValue("@inbody_seq", inbodyInfo.Inbody_seq);
							cmd.Parameters.AddWithValue("@school_grade", student.Grade);
							cmd.Parameters.AddWithValue("@school_grade_id", student.Grade_ID);
							cmd.Parameters.AddWithValue("@school_ban", student.ClassNumber);
							cmd.Parameters.AddWithValue("@measure_date", student.Measure_Date);
						}

						cmd.ExecuteNonQuery();
					}
				}

				if (result)
				{
					tx.Commit();
				}
				else
				{
					tx.Rollback();
				}
			}
			catch (Exception e)
			{
				if (tx != null)
				{
					tx.Rollback();
				}

				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
				return false;
			}
			finally
			{
				conn.Close();

				if (tx != null)
				{
					tx.Dispose();
				}
			}

			return result;
		}

		public bool InsertInbodyMeasureInfo(VoStudent student, VoInbody inbodyInfo)
		{
			bool result = true;

			MySqlConnection conn = new MySqlConnection();
			conn.ConnectionString = this._connectionString;

			MySqlCommand cmd = new MySqlCommand();
			MySqlTransaction tx = null;
			MySqlDataAdapter sqlAdapter = null;

			string querySelectInbodySeq = "select ifnull(max(Inbody_seq + 1), 0) as inbody_seq from inbody_info";

			string queryInsertInbodyInfo =
				"INSERT INTO INBODY_INFO(INBODY_SEQ, DATETIMES, Weight, Protein_Mass, Mineral_Mass, Body_Fat_Mass" +
				", Total_Body_Water, Soft_Lean_Mass, Fat_Free_Mass, OSSEOUS, Skeletal_Muscle_Mass, BMI" +
				", Percent_Body_Fat, Waist_Hip_Ratio, Target_Weight,Weight_Control, Fat_Control, Muscle_Control" +
				", Fitness_Score, BMR, Neck, Chest, ABD, THIGHL, ACL, HIP, THIGHR, ACR, Protein_Max, Protein_Min" +
				", Mineral_Max, Mineral_Min, Body_Fat_Mass_Max, Body_Fat_Mass_Min, Weight_Max, Weight_Min" +
				", Skeletal_Muscle_Mass_Max, Skeletal_Muscle_Mass_Min, BMI_Max, BMI_Min, Percent_Body_Fat_Max" +
				", Percent_Body_Fat_Min, Waist_Hip_Ratio_Max, Waist_Hip_Ratio_Min, BMR_MAX, BMR_MIN, FCHEST" +
				", FABD, FACR, FACL, FTHIGHR, FTHIGHL, HEIGHT)" +
				"VALUES(@inbody_seq, @datetimes, @weight, @protein_Mass, @mineral_Mass, @body_Fat_Mass, @total_Body_Water" +
				", @soft_Lean_Mass, @fat_Free_Mass, @oSSEOUS, @skeletal_Muscle_Mass, @bMI, @percent_Body_Fat, @waist_Hip_Ratio" +
				", @target_Weight, @weight_Control, @fat_Control, @muscle_Control, @fitness_Score, @bMR, @neck, @chest, @aBD, @tHIGHL" +
				", @aCL, @hIP, @tHIGHR, @aCR, @protein_Max, @protein_Min, @mineral_Max, @mineral_Min, @body_Fat_Mass_Max, @body_Fat_Mass_Min" +
				", @weight_Max, @weight_Min, @skeletal_Muscle_Mass_Max, @skeletal_Muscle_Mass_Min, @bMI_Max, @bMI_Min, @percent_Body_Fat_Max" +
				", @percent_Body_Fat_Min, @waist_Hip_Ratio_Max, @waist_Hip_Ratio_Min, @bMR_MAX, @bMR_MIN, @fCHEST, @fABD, @fACR, @fACL, @fTHIGHR" +
				", @fTHIGHL, @hEIGHT)";

			string queryInsertMeasureInfo = @"	INSERT INTO MEASURE_INFO (member_id, school_id, inbody_seq, school_grade, school_grade_id, school_ban, measure_date, created_date)
												VALUES (@member_id, @school_id, @inbody_seq, @school_grade, @school_grade_id, @school_ban, @measure_date, now())";
			string queryUpdateMeasureInfo = @"	UPDATE MEASURE_INFO SET inbody_seq = @inbody_seq, measure_date = @measure_date, created_date = now()
													, school_grade = @school_grade, school_grade_id = @school_grade_id, school_ban = @school_ban, school_id = @school_id
												WHERE measure_id = @measure_id";

			try
			{
				conn.Open();
				cmd.Connection = conn;
				tx = conn.BeginTransaction();

				if (result)
				{
					cmd.CommandText = querySelectInbodySeq;

					sqlAdapter = new MySqlDataAdapter(cmd.CommandText, conn);

					DataTable dt = new DataTable();
					sqlAdapter.Fill(dt);

					if (dt.Rows.Count > 0)
					{
						cmd.CommandText = queryInsertInbodyInfo;
						cmd.Parameters.Clear();

						// Inbody 데이터 저장
						inbodyInfo.Inbody_seq = dt.Rows[0][0].ToString();

						cmd.Parameters.AddWithValue("@inbody_seq", inbodyInfo.Inbody_seq);
						cmd.Parameters.AddWithValue("@datetimes", inbodyInfo.DateTimes);
						cmd.Parameters.AddWithValue("@weight", inbodyInfo.Weight);
						cmd.Parameters.AddWithValue("@protein_Mass", inbodyInfo.Protein_Mass);
						cmd.Parameters.AddWithValue("@mineral_Mass", inbodyInfo.Mineral_Mass);
						cmd.Parameters.AddWithValue("@body_Fat_Mass", inbodyInfo.Body_Fat_Mass);
						cmd.Parameters.AddWithValue("@total_Body_Water", inbodyInfo.Total_Body_Water);
						cmd.Parameters.AddWithValue("@soft_Lean_Mass", inbodyInfo.Soft_Lean_Mass);
						cmd.Parameters.AddWithValue("@fat_Free_Mass", inbodyInfo.Fat_Free_Mass);
						cmd.Parameters.AddWithValue("@oSSEOUS", inbodyInfo.OSSEOUS);
						cmd.Parameters.AddWithValue("@skeletal_Muscle_Mass", inbodyInfo.Skeletal_Muscle_Mass);
						cmd.Parameters.AddWithValue("@bMI", inbodyInfo.BMI);
						cmd.Parameters.AddWithValue("@percent_Body_Fat", inbodyInfo.Percent_Body_Fat);
						cmd.Parameters.AddWithValue("@waist_Hip_Ratio", inbodyInfo.Waist_Hip_Ratio);
						cmd.Parameters.AddWithValue("@target_Weight", inbodyInfo.Target_Weight);
						cmd.Parameters.AddWithValue("@weight_Control", inbodyInfo.Weight_Control);
						cmd.Parameters.AddWithValue("@fat_Control", inbodyInfo.Fat_Control);
						cmd.Parameters.AddWithValue("@muscle_Control", inbodyInfo.Muscle_Control);
						cmd.Parameters.AddWithValue("@fitness_Score", inbodyInfo.Fitness_Score);
						cmd.Parameters.AddWithValue("@bMR", inbodyInfo.BMR);
						cmd.Parameters.AddWithValue("@neck", inbodyInfo.Neck);
						cmd.Parameters.AddWithValue("@chest", inbodyInfo.Chest);
						cmd.Parameters.AddWithValue("@aBD", inbodyInfo.ABD);
						cmd.Parameters.AddWithValue("@tHIGHL", inbodyInfo.THIGHL);
						cmd.Parameters.AddWithValue("@aCL", inbodyInfo.ACL);
						cmd.Parameters.AddWithValue("@hIP", inbodyInfo.HIP);
						cmd.Parameters.AddWithValue("@tHIGHR", inbodyInfo.THIGHR);
						cmd.Parameters.AddWithValue("@aCR", inbodyInfo.ACR);
						cmd.Parameters.AddWithValue("@protein_Max", inbodyInfo.Protein_Max);
						cmd.Parameters.AddWithValue("@protein_Min", inbodyInfo.Protein_Min);
						cmd.Parameters.AddWithValue("@mineral_Max", inbodyInfo.Mineral_Max);
						cmd.Parameters.AddWithValue("@mineral_Min", inbodyInfo.Mineral_Min);
						cmd.Parameters.AddWithValue("@body_Fat_Mass_Max", inbodyInfo.Body_Fat_Mass_Max);
						cmd.Parameters.AddWithValue("@body_Fat_Mass_Min", inbodyInfo.Body_Fat_Mass_Min);
						cmd.Parameters.AddWithValue("@weight_Max", inbodyInfo.Weight_Max);
						cmd.Parameters.AddWithValue("@weight_Min", inbodyInfo.Weight_Min);
						cmd.Parameters.AddWithValue("@skeletal_Muscle_Mass_Max", inbodyInfo.Skeletal_Muscle_Mass_Max);
						cmd.Parameters.AddWithValue("@skeletal_Muscle_Mass_Min", inbodyInfo.Skeletal_Muscle_Mass_Min);
						cmd.Parameters.AddWithValue("@bMI_Max", inbodyInfo.BMI_Max);
						cmd.Parameters.AddWithValue("@bMI_Min", inbodyInfo.BMI_Min);
						cmd.Parameters.AddWithValue("@percent_Body_Fat_Max", inbodyInfo.Percent_Body_Fat_Max);
						cmd.Parameters.AddWithValue("@percent_Body_Fat_Min", inbodyInfo.Percent_Body_Fat_Min);
						cmd.Parameters.AddWithValue("@waist_Hip_Ratio_Max", inbodyInfo.Waist_Hip_Ratio_Max);
						cmd.Parameters.AddWithValue("@waist_Hip_Ratio_Min", inbodyInfo.Waist_Hip_Ratio_Min);
						cmd.Parameters.AddWithValue("@bMR_MAX", inbodyInfo.BMR_MAX);
						cmd.Parameters.AddWithValue("@bMR_MIN", inbodyInfo.BMR_MIN);
						cmd.Parameters.AddWithValue("@fCHEST", inbodyInfo.FCHEST);
						cmd.Parameters.AddWithValue("@fABD", inbodyInfo.FABD);
						cmd.Parameters.AddWithValue("@fACR", inbodyInfo.FACR);
						cmd.Parameters.AddWithValue("@fACL", inbodyInfo.FACL);
						cmd.Parameters.AddWithValue("@fTHIGHR", inbodyInfo.FTHIGHR);
						cmd.Parameters.AddWithValue("@fTHIGHL", inbodyInfo.FTHIGHL);
						cmd.Parameters.AddWithValue("@hEIGHT", inbodyInfo.HEIGHT);

						cmd.ExecuteNonQuery();
					}
					else
					{
						result = false;
					}
				}

				if (result)
				{
					// 측정 정보 기록
					bool isUpdateMeasureInfo = false;

					// 이전 측정 정보 기록이 있을 때
					if (!String.IsNullOrWhiteSpace(student.LastMeasureDate))
					{
						DateTime now_date = DateTime.Now;
						DateTime last_measure_date = DateTime.Parse(student.LastMeasureDate);

						// 이전 측정 정보 기록일이 현재의 월과 같은 경우에는 업데이트
						if (now_date.Year == last_measure_date.Year && now_date.Month == last_measure_date.Month)
						{
							isUpdateMeasureInfo = true;
						}
					}

					if (result)
					{
						// UPDATE
						if (isUpdateMeasureInfo)
						{
							cmd.CommandText = queryUpdateMeasureInfo;
							cmd.Parameters.Clear();

							cmd.Parameters.AddWithValue("@inbody_seq", inbodyInfo.Inbody_seq);
							cmd.Parameters.AddWithValue("@measure_date", student.Measure_Date);
							cmd.Parameters.AddWithValue("@school_id", student.SchoolID);
							cmd.Parameters.AddWithValue("@school_grade", student.Grade);
							cmd.Parameters.AddWithValue("@school_grade_id", student.Grade_ID);
							cmd.Parameters.AddWithValue("@school_ban", student.ClassNumber);
							cmd.Parameters.AddWithValue("@measure_id", student.UpdateMeasureSeq);
						}
						else
						{
							cmd.CommandText = queryInsertMeasureInfo;
							cmd.Parameters.Clear();

							cmd.Parameters.AddWithValue("@member_id", student.ID);
							cmd.Parameters.AddWithValue("@school_id", student.SchoolID);
							cmd.Parameters.AddWithValue("@inbody_seq", inbodyInfo.Inbody_seq);
							cmd.Parameters.AddWithValue("@school_grade", student.Grade);
							cmd.Parameters.AddWithValue("@school_grade_id", student.Grade_ID);
							cmd.Parameters.AddWithValue("@school_ban", student.ClassNumber);
							cmd.Parameters.AddWithValue("@measure_date", student.Measure_Date);
						}

						cmd.ExecuteNonQuery();
					}
				}

				if (result)
				{
					tx.Commit();
				}
				else
				{
					tx.Rollback();
				}
			}
			catch (Exception e)
			{
				if (tx != null)
				{
					tx.Rollback();
				}

				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
				return false;
			}
			finally
			{
				conn.Close();

				if (tx != null)
				{
					tx.Dispose();
				}
			}

			return result;
		}

		public bool InsertSmokeMeasureInfo(VoStudent student, VoSmokeInfo smokeInfo)
		{
			bool result = true;

			MySqlConnection conn = new MySqlConnection();
			conn.ConnectionString = this._connectionString;

			MySqlCommand cmd = new MySqlCommand();
			MySqlTransaction tx = null;
			MySqlDataAdapter sqlAdapter = null;

			string querySelectSmokeSeq = "select ifnull(max(smoke_seq + 1), 0) as smoke_seq from smoke_info";

			string queryInsertSmokeInfo =
							"INSERT INTO SMOKE_INFO (SMOKE_SEQ, PPM, COHD, DATETIMES) " +
							"VALUES(@smoke_seq, @ppm, @cohd, now())";

			string queryInsertMeasureInfo = @"	INSERT INTO MEASURE_INFO (member_id, school_id, smoke_seq, school_grade, school_grade_id, school_ban, measure_date, created_date)
												VALUES (@member_id, @school_id, @smoke_seq, @school_grade, @school_grade_id, @school_ban, @measure_date, now())";
			string queryUpdateMeasureInfo = @"	UPDATE MEASURE_INFO SET smoke_seq = @smoke_seq, measure_date = @measure_date, created_date = now()
													, school_grade = @school_grade, school_grade_id = @school_grade_id, school_ban = @school_ban, school_id = @school_id
												WHERE measure_id = @measure_id";

			try
			{
				conn.Open();
				cmd.Connection = conn;
				tx = conn.BeginTransaction();

				// 흡연 정보가 있을 경우
				if (result && !String.IsNullOrWhiteSpace(smokeInfo.COHD))
				{
					// Smoke 데이터 저장
					cmd.CommandText = querySelectSmokeSeq;

					sqlAdapter = new MySqlDataAdapter(cmd.CommandText, conn);

					DataTable dt = new DataTable();
					sqlAdapter.Fill(dt);

					if (dt.Rows.Count > 0)
					{
						cmd.CommandText = queryInsertSmokeInfo;
						cmd.Parameters.Clear();

						// Smoke 데이터 저장
						smokeInfo.SmokeSeq = dt.Rows[0][0].ToString();

						cmd.Parameters.AddWithValue("@smoke_seq", smokeInfo.SmokeSeq);
						cmd.Parameters.AddWithValue("@ppm", smokeInfo.PPM);
						cmd.Parameters.AddWithValue("@cohd", smokeInfo.COHD);

						cmd.ExecuteNonQuery();
					}
					else
					{
						result = false;
					}
				}

				if (result)
				{
					// 측정 정보 기록
					bool isUpdateMeasureInfo = false;

					// 이전 측정 정보 기록이 있을 때
					if (!String.IsNullOrWhiteSpace(student.LastMeasureDate))
					{
						DateTime now_date = DateTime.Now;
						DateTime last_measure_date = DateTime.Parse(student.LastMeasureDate);

						// 이전 측정 정보 기록일이 현재의 월과 같은 경우에는 업데이트
						if (now_date.Year == last_measure_date.Year && now_date.Month == last_measure_date.Month)
						{
							isUpdateMeasureInfo = true;
						}
					}

					if (result)
					{
						// UPDATE
						if (isUpdateMeasureInfo)
						{
							cmd.CommandText = queryUpdateMeasureInfo;
							cmd.Parameters.Clear();

							cmd.Parameters.Add("@smoke_seq", MySqlDbType.Int32);

							if (!String.IsNullOrWhiteSpace(smokeInfo.SmokeSeq))
							{
								cmd.Parameters["@smoke_seq"].Value = smokeInfo.SmokeSeq;
							}
							else
							{
								cmd.Parameters["@smoke_seq"].Value = DBNull.Value;
							}

							cmd.Parameters.AddWithValue("@measure_date", student.Measure_Date);
							cmd.Parameters.AddWithValue("@school_id", student.SchoolID);
							cmd.Parameters.AddWithValue("@school_grade", student.Grade);
							cmd.Parameters.AddWithValue("@school_grade_id", student.Grade_ID);
							cmd.Parameters.AddWithValue("@school_ban", student.ClassNumber);
							cmd.Parameters.AddWithValue("@measure_id", student.UpdateMeasureSeq);
						}
						else
						{
							cmd.CommandText = queryInsertMeasureInfo;
							cmd.Parameters.Clear();

							cmd.Parameters.AddWithValue("@member_id", student.ID);
							cmd.Parameters.AddWithValue("@school_id", student.SchoolID);

							cmd.Parameters.Add("@smoke_seq", MySqlDbType.Int32);

							if (!String.IsNullOrWhiteSpace(smokeInfo.SmokeSeq))
							{
								cmd.Parameters["@smoke_seq"].Value = smokeInfo.SmokeSeq;
							}
							else
							{
								cmd.Parameters["@smoke_seq"].Value = DBNull.Value;
							}

							cmd.Parameters.AddWithValue("@school_grade", student.Grade);
							cmd.Parameters.AddWithValue("@school_grade_id", student.Grade_ID);
							cmd.Parameters.AddWithValue("@school_ban", student.ClassNumber);
							cmd.Parameters.AddWithValue("@measure_date", student.Measure_Date);
						}

						cmd.ExecuteNonQuery();
					}
				}

				if (result)
				{
					tx.Commit();
				}
				else
				{
					tx.Rollback();
				}
			}
			catch (Exception e)
			{
				if (tx != null)
				{
					tx.Rollback();
				}

				_mainForm._isError = true;
				_mainForm.ShowNotice(e.Message);
				return false;
			}
			finally
			{
				conn.Close();

				if (tx != null)
				{
					tx.Dispose();
				}
			}

			return result;
		}
	}
}
