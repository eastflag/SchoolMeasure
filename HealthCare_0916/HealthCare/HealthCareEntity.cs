using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthCare
{
	public class VoSmokeInfo
	{
		public string SmokeSeq
		{
			get;
			set;
		}
		public string PPM { get; set; }
		public string COHD { get; set; }
		public string DATETIMES { get; set; }

		public VoSmokeInfo()
		{
			this.SmokeSeq = "";
			this.PPM = "";
			this.COHD = "";
			this.DATETIMES = DateTime.Now.ToString("yyyy-MM-dd");
		}
	}

	public class VoStudent
	{
		public VoStudent() 
		{
			this.id = "";
		}

		string id;
		public string ID
		{
			get { return id; }
			set { id = value; }
		}

		string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		string schoolId;
		public string SchoolID
		{
			get { return schoolId; }
			set { schoolId = value; }
		}

		string grade;
		public string Grade
		{
			get { return grade; }
			set { grade = value; }
		}

		string grade_ID;
		public string Grade_ID
		{
			get { return grade_ID; }
			set { grade_ID = value; }
		}

		string classNumber;
		public string ClassNumber
		{
			get { return classNumber; }
			set { classNumber = value; }
		}

		string measure_date;
		public string Measure_Date
		{
			get { return measure_date; }
			set { measure_date = value; }
		}

		string update_measure_seq;
		public string UpdateMeasureSeq
		{
			get
			{
				return update_measure_seq;
			}
			set
			{
				update_measure_seq = value;
			}
		}

		string last_measure_date;
		public string LastMeasureDate
		{
			get
			{
				return last_measure_date;
			}
			set
			{
				last_measure_date = value;
			}
		}
	}

	public class VoInbody
	{
		string inbody_seq;
		public string Inbody_seq
		{
			get { return inbody_seq; }
			set { inbody_seq = value; }
		}

		string dateTimes;
		public string DateTimes
		{
			get { return dateTimes; }
			set { dateTimes = value; }
		}

		string weight;
		public string Weight
		{
			get { return weight; }
			set { weight = value; }
		}

		string protein_Mass;
		public string Protein_Mass
		{
			get { return protein_Mass; }
			set { protein_Mass = value; }
		}

		string mineral_Mass;
		public string Mineral_Mass
		{
			get { return mineral_Mass; }
			set { mineral_Mass = value; }
		}

		string body_Fat_Mass;
		public string Body_Fat_Mass
		{
			get { return body_Fat_Mass; }
			set { body_Fat_Mass = value; }
		}

		string total_Body_Water;
		public string Total_Body_Water
		{
			get { return total_Body_Water; }
			set { total_Body_Water = value; }
		}

		string soft_Lean_Mass;
		public string Soft_Lean_Mass
		{
			get { return soft_Lean_Mass; }
			set { soft_Lean_Mass = value; }
		}

		string fat_Free_Mass;
		public string Fat_Free_Mass
		{
			get { return fat_Free_Mass; }
			set { fat_Free_Mass = value; }
		}

		string oSSEOUS;
		public string OSSEOUS
		{
			get { return oSSEOUS; }
			set { oSSEOUS = value; }
		}

		string skeletal_Muscle_Mass;
		public string Skeletal_Muscle_Mass
		{
			get { return skeletal_Muscle_Mass; }
			set { skeletal_Muscle_Mass = value; }
		}

		string bMI;
		public string BMI
		{
			get { return bMI; }
			set { bMI = value; }
		}

		string percent_Body_Fat;
		public string Percent_Body_Fat
		{
			get { return percent_Body_Fat; }
			set { percent_Body_Fat = value; }
		}

		string waist_Hip_Ratio;
		public string Waist_Hip_Ratio
		{
			get { return waist_Hip_Ratio; }
			set { waist_Hip_Ratio = value; }
		}

		string target_Weight;
		public string Target_Weight
		{
			get { return target_Weight; }
			set { target_Weight = value; }
		}

		string weight_Control;
		public string Weight_Control
		{
			get { return weight_Control; }
			set { weight_Control = value; }
		}

		string fat_Control;
		public string Fat_Control
		{
			get { return fat_Control; }
			set { fat_Control = value; }
		}

		string muscle_Control;
		public string Muscle_Control
		{
			get { return muscle_Control; }
			set { muscle_Control = value; }
		}

		string fitness_Score;
		public string Fitness_Score
		{
			get { return fitness_Score; }
			set { fitness_Score = value; }
		}

		string bMR;
		public string BMR
		{
			get { return bMR; }
			set { bMR = value; }
		}

		string neck;
		public string Neck
		{
			get { return neck; }
			set { neck = value; }
		}

		string chest;
		public string Chest
		{
			get { return chest; }
			set { chest = value; }
		}

		string aBD;
		public string ABD
		{
			get { return aBD; }
			set { aBD = value; }
		}

		string tHIGHL;
		public string THIGHL
		{
			get { return tHIGHL; }
			set { tHIGHL = value; }
		}

		string aCL;
		public string ACL
		{
			get { return aCL; }
			set { aCL = value; }
		}

		string hIP;
		public string HIP
		{
			get { return hIP; }
			set { hIP = value; }
		}

		string tHIGHR;
		public string THIGHR
		{
			get { return tHIGHR; }
			set { tHIGHR = value; }
		}

		string aCR;
		public string ACR
		{
			get { return aCR; }
			set { aCR = value; }
		}

		string protein_Max;
		public string Protein_Max
		{
			get { return protein_Max; }
			set { protein_Max = value; }
		}

		string protein_Min;
		public string Protein_Min
		{
			get { return protein_Min; }
			set { protein_Min = value; }
		}

		string mineral_Max;
		public string Mineral_Max
		{
			get { return mineral_Max; }
			set { mineral_Max = value; }
		}

		string mineral_Min;
		public string Mineral_Min
		{
			get { return mineral_Min; }
			set { mineral_Min = value; }
		}

		string body_Fat_Mass_Max;
		public string Body_Fat_Mass_Max
		{
			get { return body_Fat_Mass_Max; }
			set { body_Fat_Mass_Max = value; }
		}

		string body_Fat_Mass_Min;
		public string Body_Fat_Mass_Min
		{
			get { return body_Fat_Mass_Min; }
			set { body_Fat_Mass_Min = value; }
		}

		string weight_Max;
		public string Weight_Max
		{
			get { return weight_Max; }
			set { weight_Max = value; }
		}

		string weight_Min;
		public string Weight_Min
		{
			get { return weight_Min; }
			set { weight_Min = value; }
		}

		string skeletal_Muscle_Mass_Max;
		public string Skeletal_Muscle_Mass_Max
		{
			get { return skeletal_Muscle_Mass_Max; }
			set { skeletal_Muscle_Mass_Max = value; }
		}

		string skeletal_Muscle_Mass_Min;
		public string Skeletal_Muscle_Mass_Min
		{
			get { return skeletal_Muscle_Mass_Min; }
			set { skeletal_Muscle_Mass_Min = value; }
		}

		string bMI_Max;
		public string BMI_Max
		{
			get { return bMI_Max; }
			set { bMI_Max = value; }
		}

		string bMI_Min;
		public string BMI_Min
		{
			get { return bMI_Min; }
			set { bMI_Min = value; }
		}

		string percent_Body_Fat_Max;
		public string Percent_Body_Fat_Max
		{
			get { return percent_Body_Fat_Max; }
			set { percent_Body_Fat_Max = value; }
		}

		string percent_Body_Fat_Min;
		public string Percent_Body_Fat_Min
		{
			get { return percent_Body_Fat_Min; }
			set { percent_Body_Fat_Min = value; }
		}

		string waist_Hip_Ratio_Max;
		public string Waist_Hip_Ratio_Max
		{
			get { return waist_Hip_Ratio_Max; }
			set { waist_Hip_Ratio_Max = value; }
		}

		string waist_Hip_Ratio_Min;
		public string Waist_Hip_Ratio_Min
		{
			get { return waist_Hip_Ratio_Min; }
			set { waist_Hip_Ratio_Min = value; }
		}

		string bMR_MAX;
		public string BMR_MAX
		{
			get { return bMR_MAX; }
			set { bMR_MAX = value; }
		}

		string bMR_MIN;
		public string BMR_MIN
		{
			get { return bMR_MIN; }
			set { bMR_MIN = value; }
		}

		string fCHEST;
		public string FCHEST
		{
			get { return fCHEST; }
			set { fCHEST = value; }
		}

		string fABD;
		public string FABD
		{
			get { return fABD; }
			set { fABD = value; }
		}

		string fACR;
		public string FACR
		{
			get { return fACR; }
			set { fACR = value; }
		}

		string fACL;
		public string FACL
		{
			get { return fACL; }
			set { fACL = value; }
		}

		string fTHIGHR;
		public string FTHIGHR
		{
			get { return fTHIGHR; }
			set { fTHIGHR = value; }
		}

		string fTHIGHL;
		public string FTHIGHL
		{
			get { return fTHIGHL; }
			set { fTHIGHL = value; }
		}

		string hEIGHT;
		public string HEIGHT
		{
			get { return hEIGHT; }
			set { hEIGHT = value; }
		}

		string studentId;
		public string StudentId
		{
			get { return studentId; }
			set { studentId = value; }
		}

		string measure_date;
		public string Measure_Date
		{
			get { return measure_date; }
			set { measure_date = value; }
		}

	}
}


