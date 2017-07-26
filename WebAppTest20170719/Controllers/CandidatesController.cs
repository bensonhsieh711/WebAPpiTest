using SR.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using WebAppTest20170719.Models;

namespace WebAppTest20170719.Controllers
{
    public class CandidatesController : ApiController
    {
        Models.Candidate[] Candidates = new Models.Candidate[]
        {
            new Models.Candidate {Name = "Benson", Age = 28, Email = "Benson_Hsieh@syscom.com.tw", Id = "153803"},
            new Models.Candidate {Name = "Jimmy", Age = 33, Email = "Jimmy_Hung@syscom.com.tw", Id = "990011"},
            new Models.Candidate {Name = "Joey", Age = 3, Email = "Joey_Lin@syscom.com.tw", Id = "140001"}
        };

        //取得所有應徵者的資料清單
        public IEnumerable<Models.Candidate> GetAllCandidates()
        {
            return Candidates;
        }

        
        //[HttpPost]
        //[Route("candidates/GetCandidate/{Name}")]
        ////新增應徵者的資料
        //public IHttpActionResult GetCandidate(string Name)
        //{
        //    var myCandidate = Candidates.FirstOrDefault((c) => c.Name == Name);
        //    if (myCandidate == null)
        //        return StatusCode(HttpStatusCode.NoContent);
        //    else
        //        return Ok(myCandidate);
        //}

        //[HttpGet]
        //[Route("candidates/GetCandidate/{Name}")]
        ////以姓名搜尋應徵者資料
        //public IHttpActionResult GetCandidateByName(string Name)
        //{
        //    var myCandidate = Candidates.FirstOrDefault((c) => c.Name == Name);
        //    if (myCandidate == null)
        //        return NotFound();
        //    else
        //        return Ok(myCandidate);
        //}

        ////以ID搜尋應徵者資料
        //public IHttpActionResult GetCandidateById(string Id)
        //{
        //    var myCandidate = Candidates.FirstOrDefault((c) => c.Id == Id);
        //    if (myCandidate == null)
        //        return NotFound();
        //    else
        //        return Ok(myCandidate);
        //}

        ////以姓名及ID搜尋應徵者資料
        //public IHttpActionResult GetCandidateByNameaAndId(string Name, string Id)
        //{
        //    var myCandidate = Candidates.FirstOrDefault((c) => c.Name == Name && c.Id == Id);
        //    if (myCandidate == null)
        //        return NotFound();
        //    else
        //        return Ok(myCandidate);
        //}

        [HttpGet]
        [Route("candidates/GetCandidateByCondition/{Name}/{Age}/{Email}/{Id}")]
        //搜尋應徵者資料(包含查詢條件)
        public IHttpActionResult GetCandidateByCondition(string Name = "", int? Age = null, string Email = "", string Id = "")
        //public List<Candidate> GetCandidate([FromUri]Candidate candidates)
        {
            // 選擇欲使用之資料庫
            string dbConnString = ConfigurationManager.ConnectionStrings["G8"].ConnectionString;

            try
            {
                System.Text.StringBuilder sql = new System.Text.StringBuilder();                

                sql.Append("SELECT * FROM Candidates WHERE (1=1) ");                

                // 參數
                //ArrayList parameters = new ArrayList();                

                if (!string.IsNullOrEmpty(Name))
                {
                    sql.Append(string.Format(" AND Name = '{0}'", Name));
                    //parameters.Add(Name);
                }

                if (Age.HasValue)
                {
                    sql.Append(string.Format(" AND Age = '{0}'", Age));
                    //parameters.Add(Age);
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    sql.Append(string.Format(" AND Email = '{0}'", Email));
                    //parameters.Add(Email);
                }

                if (!string.IsNullOrEmpty(Id))
                {
                    sql.Append(string.Format(" AND Id = '{0}'", Id));
                    //parameters.Add(Id);
                }

                //var result = db.Read(sql.ToString(), false, parameters);
                var mr = new SqlConnection(dbConnString);
                var result = mr.Query<Candidate>(sql.ToString());
                
                if (result != null && result.Count() > 0)
                {
                    return Ok(result.ToList());
                }
                return Ok("查無資料！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("candidates/GetCandidate/")]
        //搜尋應徵者資料
        public IHttpActionResult GetCandidate(string Name = "", int? Age = null, string Email = "", string Id = "")        
        {
            // 選擇欲使用之資料庫
            string dbConnString = ConfigurationManager.ConnectionStrings["G8"].ConnectionString;

            try
            {
                System.Text.StringBuilder sql = new System.Text.StringBuilder();

                sql.Append("SELECT * FROM Candidates WHERE (1=1) ");

                // 參數
                //ArrayList parameters = new ArrayList();                

                if (!string.IsNullOrEmpty(Name))
                {
                    sql.Append(string.Format(" AND Name = '{0}'", Name));
                    //parameters.Add(Name);
                }

                if (Age.HasValue)
                {
                    sql.Append(string.Format(" AND Age = '{0}'", Age));
                    //parameters.Add(Age);
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    sql.Append(string.Format(" AND Email = '{0}'", Email));
                    //parameters.Add(Email);
                }

                if (!string.IsNullOrEmpty(Id))
                {
                    sql.Append(string.Format(" AND Id = '{0}'", Id));
                    //parameters.Add(Id);
                }

                //var result = db.Read(sql.ToString(), false, parameters);
                var mr = new SqlConnection(dbConnString);
                var result = mr.Query<Candidate>(sql.ToString());

                if (result != null && result.Count() > 0)
                {
                    return Ok(result.ToList());
                }
                return Ok("查無資料！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //新增應徵者資料
        [HttpPost]
        [Route("candidates/PostCandidate")]
        public IHttpActionResult PostCandidate([FromBody]Candidate candidate)
        {
            // 選擇欲使用之資料庫
            DbHandler db = DbHandler.Instance("G8");

            int resultNum = 0;

            try
            {
                System.Text.StringBuilder sql = new System.Text.StringBuilder();

                sql.Append(String.Format("INSERT INTO Candidates ( Name, Age, Email, Id  ) values ( '{0}', '{1}', '{2}', '{3}' )",
                    candidate.Name, candidate.Age.ToString(), candidate.Email, candidate.Id));

                resultNum = db.Write(sql.ToString(), false);

                return Ok(string.Format("執行成功，共新增{0}筆資料！", resultNum));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //更新應徵者資料
        [HttpPut]
        [Route("candidates/PutCandidate")]
        public IHttpActionResult PutCandidate([FromBody]Candidate candidate)
        {
            // 選擇欲使用之資料庫
            DbHandler db = DbHandler.Instance("G8");
            int resultNum = 0;

            try
            {
                System.Text.StringBuilder sql = new System.Text.StringBuilder();
                sql.Append("UPDATE  Candidates ");
                sql.Append("  SET Name = ?, Age = ?, Email = ? Where Id = ?  ");

                // 參數
                ArrayList parameters = new ArrayList();                                
                parameters.Add(candidate.Name);
                parameters.Add(candidate.Age.ToString());
                parameters.Add(candidate.Email);
                parameters.Add(candidate.Id);
                
                object[] p = parameters.ToArray();

                // 執行SQL
                resultNum = db.Write(sql.ToString(), false, p);

                return Ok(string.Format("執行成功，共更新{0}筆資料！", resultNum));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //刪除應徵者資料
        [HttpDelete]
        [Route("candidates/DeleteCandidate")]
        public IHttpActionResult DeleteCandidate([FromBody]Candidate candidate)
        {
            // 選擇欲使用之資料庫
            DbHandler db = DbHandler.Instance("G8");
            int resultNum = 0;

            try
            {
                System.Text.StringBuilder sql = new System.Text.StringBuilder();
                sql.Append("DELETE Candidates ");
                sql.Append(" WHERE Id = ? ");

                // 參數
                ArrayList parameters = new ArrayList();
                //parameters.Add(candidate.Name);
                //parameters.Add(candidate.Age.ToString());
                //parameters.Add(candidate.Email);
                parameters.Add(candidate.Id);

                object[] p = parameters.ToArray();

                // 執行SQL
                resultNum = db.Write(sql.ToString(), false, p);

                return Ok(string.Format("執行成功，共刪除{0}筆資料！", resultNum));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
    
}
