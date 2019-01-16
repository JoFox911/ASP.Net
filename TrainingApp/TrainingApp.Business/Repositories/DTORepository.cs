using System;
using System.Linq;
using TrainingApp.Data.Contexts;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;



namespace TrainingApp.Business.Repositories
{
    public class DTORepository<TDTO> where TDTO : class
    {
        public readonly TrainingAppDbContext Context;
        //public IUserIdentInfo UserInfo => Context.UserInfo;
        //public bool ListModelCounted => _listModelCounted;

        private static string QueryTextString;
        //private static readonly bool _listModelCounted;

        //static DTORepository()
        //{
        //    _listModelCounted = typeof(IPagingCounted).IsAssignableFrom(typeof(TDTO));
        //}

        private string QueryString
        {
            get
            {
                if (QueryTextString == null)
                {
                    var dtoType = typeof(TDTO);
                    var assembly = Assembly.GetAssembly(dtoType);
                    var resourceName = dtoType.Namespace + "." + dtoType.Name + ".sql";
                    
                    resourceName = assembly.GetManifestResourceNames()
                        .FirstOrDefault(r => r.ToLowerInvariant() == resourceName.ToLowerInvariant());

                    if (resourceName == null)
                    {
                        throw new Exception($"Resource {dtoType.Name} not found!");
                    }

                    string tempQueryString = null;

                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            tempQueryString = reader.ReadToEnd();
                        }
                    }

                    //var query_option_raw_execute = tempQueryString.Contains("query_option_raw_execute");
                    //var alias = "";

                    //if (!query_option_raw_execute)
                    //{
                    //    alias = "qry_x";
                    //    tempQueryString = "select *" +
                    //                //(StaticListModelCounted ? ", count(*) over() as \"TotalRecordCount\"" : "") +
                    //                (_listModelCounted ? ", count(*) over() as total_record_count" : "") +
                    //                " from (" + tempQueryString + ") " + alias;

                    //    if (!tempQueryString.EndsWith("{predicate}"))
                    //    {
                    //        tempQueryString += " {predicate}";
                    //    }
                    //}

                    //var rightsQueryString = GetRightsQueryString(alias);
                    //if (!string.IsNullOrEmpty(rightsQueryString))
                    //{
                    //    if (tempQueryString.EndsWith("{predicate}"))
                    //    {
                    //        tempQueryString += " and " + rightsQueryString;
                    //    }
                    //    else
                    //    {
                    //        tempQueryString += " where " + rightsQueryString;
                    //    }
                    //}
                    QueryTextString = tempQueryString;
                }

                return QueryTextString;
            }
        }

        public DTORepository(TrainingAppDbContext context)
        {
            Context = context;
        }

        public IQueryable<TDTO> GetDTO(string conditions, params object[] paramArray)
        {
            //var e = Context.Users;
            //var e1 = Context.Users.FromSql("SELECT * FROM users");
            //var e2 = Context.Users.FromSql("SELECT * FROM users").ToList();
            //var l = Context.UserDetailDTO;
            //var l1 = Context.UserDetailDTO.FromSql("SELECT Id, Email, Password FROM users");
            //var l2 = Context.UserDetailDTO.FromSql("SELECT u.Id, u.Email, u.Password, u.LastName, u.FirstName, u.SurName, r.Name as Role, r.Id as RoleId FROM users as u left join roles r on r.Id=u.RoleId").ToList();
            ////var t2 = Context.Query<TDTO>();
            return Context.Query<TDTO>().FromSql(QueryString/*"SELECT * FROM users"/*QueryStringParameterized(conditions, paramArray)*/);
        }

        //public string QueryStringParameterized(string conditions, params object[] paramArray)
        //{
        //    var queryString = QueryString.Replace("{predicate}", conditions);
        //    return string.Format(queryString, paramArray);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Security.NoRightsException"></exception>
        /// <param name="queryString"></param>
        /// <param name="alias"></param>
        //private string GetRightsQueryString(string alias = "")
        //{
        //    var rightsQueryString = "";
        //    var rls = UserInfo.RightsNew.GetTypeFieldsRlsRights(typeof(TDTO));
        //    foreach (var right in rls)
        //    {
        //        if (right.PermissionType == Security.RowLevelModelPermissionType.No)
        //        {
        //            // this should never happen because such restriction will be resolved when entire entity is checked
        //            throw new Security.NoRightsException();
        //        }
        //        else if (right.PermissionType == Security.RowLevelModelPermissionType.All)
        //        {
        //            continue;
        //        }
        //        else if (right.PermissionType == Security.RowLevelModelPermissionType.Specified
        //          || right.PermissionType == Security.RowLevelModelPermissionType.Except)
        //        {

        //            var idsString = "";
        //            foreach (var id in right.Entities)
        //            {
        //                if (string.IsNullOrEmpty(idsString))
        //                {
        //                    idsString += "(";
        //                }
        //                else
        //                {
        //                    idsString += ", ";
        //                }
        //                idsString += idsString + "'" + id.ToString() + "'";
        //            }

        //            if (!string.IsNullOrEmpty(idsString))
        //            {
        //                idsString += ")";
        //            }

        //            if (right.PermissionType == Security.RowLevelModelPermissionType.Specified)
        //            {
        //                idsString = "in " + idsString;
        //            }
        //            else
        //            {
        //                idsString = "not in " + idsString;
        //            }

        //            var startOfString = string.IsNullOrEmpty(alias) ? "" : alias + "." + right.Name;
        //            idsString = startOfString + " " + idsString;
        //            if (string.IsNullOrEmpty(rightsQueryString))
        //            {
        //                rightsQueryString += idsString;
        //            }
        //            else
        //            {
        //                rightsQueryString += " and " + idsString;
        //            }

        //        }
        //    }
        //    return rightsQueryString;
        //}
    }
}
