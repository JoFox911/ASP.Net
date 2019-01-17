using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TrainingApp.Data.Contexts;
using TrainingApp.Data.DTO.Base;
using TrainingApp.Data.Repositories;


namespace TrainingApp.Data.Helpers
{
    public class SelectListHelper
    {
        private readonly TrainingAppDbContext _context;
        private const int TakeCount = 50;
        private const int ExpTimeSeconds = 30;

        public SelectListHelper(TrainingAppDbContext context)
        {
            _context = context;
        }

        IQueryable<T> QueryList<T>(Expression<Func<T, bool>> expression, Type type = null) where T : class
        {
            IQueryable<T> list;
            if (typeof(BaseDTO).IsAssignableFrom(type ?? typeof(T)))
            {
                list = new DTORepository<T>(_context).GetDTO("");
            }
            else
            {
                list = _context.Set<T>();
            }

            if (expression != null)
            {
                list = list.Where(expression);
            }

            return list;
        }

        void InitSelectList(ref SelectList list, string initialSelectedValue)
        {
            if ((initialSelectedValue != null) && (list.Count() > 0))
            {
                if (initialSelectedValue == "")
                {
                    var oldList = list.ToList();
                    oldList.Insert(0, new SelectListItem("", "", true));
                    list = new SelectList(oldList, "Value", "Text", list.SelectedValue);
                }
                else
                    foreach (var item in list)
                    {
                        if (item.Value == initialSelectedValue)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
            }
        }


        public SelectList List<T>(
            string initialSelectedValue,
            Expression<Func<T, bool>> expression) where T : class
        {
            return List("Id", "Name", ListSortDirection.Ascending, initialSelectedValue, expression);
        }

        public SelectList List<T>(
            string idPropertyName = "Id",
            string textPropertyName = "Name",
            ListSortDirection sortDirection = ListSortDirection.Ascending,
            string initialSelectedValue = null,
            Expression<Func<T, bool>> expression = null,
            int take = TakeCount,
            int expirationTimeSeconds = ExpTimeSeconds) where T : class
        {
            var type = typeof(T);
            //var expressionHash = "";/*expression == null ? "" :
            //    LocalCollectionExpander.Rewrite(Evaluator.PartialEval(expression, QueryResultCache.CanBeEvaluatedLocally)).ToString().ToMd5Fingerprint();
            //    */
            //var key = type.Name + expressionHash + '/' + take; // TODO: cache already sorted list
            List<T> list = null;

            //if (_memoryCache != null && !_memoryCache.TryGetValue(key, out list))
            //{
                list = QueryList(expression).Take(TakeCount).ToList();
                //_memoryCache.Set(key, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expirationTimeSeconds)));
            //}

            var selList = new SelectList(list, idPropertyName, textPropertyName);

            if (sortDirection == ListSortDirection.Ascending)
                selList = new SelectList(selList.OrderBy(x => x.Text).ToList(), "Value", "Text");
            else
                selList = new SelectList(selList.OrderByDescending(x => x.Text).ToList(), "Value", "Text");

            InitSelectList(ref selList, initialSelectedValue);
            return selList;
        }


        //public SelectList Enum(string enumType,
        //    string initialSelectedValue = null,
        //    ListSortDirection SortDirection = ListSortDirection.Ascending,
        //    Expression<Func<EnumRecord, bool>> expression = null,
        //    int take = TakeCount,
        //    int expirationTimeSeconds = ExpTimeSeconds)
        //{
        //    var type = typeof(EnumRecord);
        //    var expressionHash = expression == null ? "" :
        //        LocalCollectionExpander.Rewrite(Evaluator.PartialEval(expression, QueryResultCache.CanBeEvaluatedLocally)).ToString().ToMd5Fingerprint();

        //    var key = type.Name + '/' + enumType + expressionHash + SortDirection + '/' + take;
        //    List<EnumRecord> list = null;

        //    var query = QueryList(expression, type).Where(x => x.EnumType.ToLower() == enumType.ToLower());

        //    if (SortDirection == ListSortDirection.Ascending)
        //        query = query.OrderBy(x => x.Name);
        //    else
        //        query = query.OrderByDescending(x => x.Name);

        //    if (_memoryCache != null && !_memoryCache.TryGetValue(key, out list))
        //    {
        //        list = query.Take(take).ToList();
        //        _memoryCache.Set(key, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expirationTimeSeconds)));
        //    }

        //    var selList = new SelectList(list, "Code", "Name");
        //    InitSelectList(ref selList, initialSelectedValue);
        //    return selList;
        //}


        //public MultiSelectList Multi<T>(
        //    IEnumerable selectedItems,
        //    Expression<Func<T, bool>> expression) where T : class
        //{
        //    return Multi(selectedItems, "Id", "Name", ListSortDirection.Ascending, expression);
        //}

        //public MultiSelectList Multi<T>(
        //    IEnumerable selectedItems = null,
        //    string idPropertyName = "Id",
        //    string textPropertyName = "Name",
        //    ListSortDirection sortDirection = ListSortDirection.Ascending,
        //    Expression<Func<T, bool>> expression = null,
        //    int take = TakeCount,
        //    int expirationTimeSeconds = ExpTimeSeconds) where T : class
        //{
        //    var type = typeof(T);
        //    var expressionHash = expression == null ? "" :
        //        LocalCollectionExpander.Rewrite(Evaluator.PartialEval(expression, QueryResultCache.CanBeEvaluatedLocally)).ToString().ToMd5Fingerprint();

        //    var key = (type.Name + expressionHash + '/' + take).ToMd5Fingerprint();  // TODO: cache already sorted list
        //    List<T> list = null;

        //    if (!_memoryCache.TryGetValue(key, out list))
        //    {
        //        list = QueryList(expression, type).Take(take).ToList();
        //        _memoryCache.Set(key, list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expirationTimeSeconds)));
        //    }

        //    var selList = new MultiSelectList(list, idPropertyName, textPropertyName, selectedItems);

        //    if (sortDirection == ListSortDirection.Ascending)
        //        selList = new MultiSelectList(selList.OrderBy(x => x.Text).ToList(), "Value", "Text", selectedItems);
        //    else
        //        selList = new MultiSelectList(selList.OrderByDescending(x => x.Text).ToList(), "Value", "Text", selectedItems);

        //    return selList;
        //}
    }
}
