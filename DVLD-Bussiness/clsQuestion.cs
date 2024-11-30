using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bussiness
{
    public class clsQuestion
    {
        //QuestionID,Question,Option1,Option3,Option4,CorrectAnswerID
        enum Mode { enAddNew = 1, enUpdate = 2 }

        Mode _Mode;

        public int QuestionID { get; set; }
        public string Question { get; set; }
        public string Option1 {  get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int CorrectAnswerID { get; set; }

        public clsQuestion()
        {
            _Mode = Mode.enAddNew;

            this.QuestionID = -1;
            this.Question = "";
            this.Option1 = "";
            this.Option2 = "";
            this.Option3 = "";
            this.Option4 = "";
            this.CorrectAnswerID = -1;
        }

        private clsQuestion(int QuestionID,string Question, string Option1, string Option2, string Option3, string Option4, int CorrectAnswerID)
        {
            _Mode = Mode.enUpdate;

            this.QuestionID = QuestionID;
            this.Question = Question;
            this.Option1 = Option1;
            this.Option2 = Option2;
            this.Option3 = Option3;
            this.Option4 = Option4;
            this.CorrectAnswerID = CorrectAnswerID;
        }

        private bool _AddNewQuestion()
        {
            this.QuestionID = clsQuestionData.AddNewQuestion(Question, Option1, Option2,Option3, Option4, CorrectAnswerID);

            return QuestionID != -1;
        }

        private bool _UpdateQuestion()
        {
            return clsQuestionData.UpdateQuestion(QuestionID, Question, Option1, Option2, Option3, Option4, CorrectAnswerID);
        }

        public static clsQuestion Find(int QuestionID)
        {
         
            string Question = "";
            string Option1 = "";
            string Option2 = "";
            string Option3 = "";
            string Option4 = "";
            int CorrectAnswerID = -1;

            if (clsQuestionData.GetQuestionInfoByID(QuestionID, ref Question, ref Option1, ref Option2, ref Option3,ref Option4,ref CorrectAnswerID))
            {
                return new clsQuestion(QuestionID,  Question,  Option1,  Option2,  Option3,  Option4, CorrectAnswerID);
            }
            else
            {
                return null;
            }
        }

        public static bool Delete(int QuestionID)
        {
            return clsQuestionData.DeleteQuestion(QuestionID);
        }

        public static DataTable GetAllQuestiones()
        {
            return clsQuestionData.GetAllQuestions();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case Mode.enAddNew:
                    if (_AddNewQuestion())
                    {
                        _Mode = Mode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Mode.enUpdate:
                    return _UpdateQuestion();

                default: return false;
            }
        }
    }
}
