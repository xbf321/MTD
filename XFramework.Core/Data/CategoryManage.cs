﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using XFramework.Model;

namespace XFramework.Data
{
    internal static class ArticleCategoryManage
    {
        #region == Insert ==
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(CategoryInfo model)
        {
            string strSQL = "INSERT INTO Categories(Language,ParentId,ParentIdList,Sort,Name,ImageUrl,LinkUrl,Introduction,CreateDateTime,Alias,TemplateType,IsShowFirstChildNode,BannerAdImageUrl,IsEnabled) VALUES(@Language,@parentId,@parentIdList,@Sort,@Name,@ImageUrl,@LinkUrl,@Introduction,GETDATE(),@Alias,@TemplateType,@IsShowFirstChildNode,@BannerAdImageUrl,@IsEnabled);SELECT @@IDENTITY;";
            SqlParameter[] parms = { 
                                    new SqlParameter("Language",SqlDbType.Int),
                                    new SqlParameter("ParentId",SqlDbType.Int),
                                    new SqlParameter("ParentIdList",SqlDbType.NVarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("LinkUrl",SqlDbType.NVarChar),
                                    new SqlParameter("Introduction",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.NVarChar),
                                    new SqlParameter("TemplateType",SqlDbType.Int),
                                    new SqlParameter("IsShowFirstChildNode",SqlDbType.Int),
                                    new SqlParameter("BannerAdImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("IsEnabled",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Int),
                                   };
            parms[0].Value = (int)model.Language;
            parms[1].Value = model.ParentId;
            parms[2].Value = model.ParentIdList;
            parms[3].Value = model.Sort;
            parms[4].Value = model.Name == null ? string.Empty : model.Name;
            parms[5].Value = model.ImageUrl == null ? string.Empty : model.ImageUrl;
            parms[6].Value = model.LinkUrl == null ? string.Empty : model.LinkUrl;
            parms[7].Value = model.Introduction == null ? string.Empty : model.Introduction;
            parms[8].Value = model.Alias == null ? string.Empty : model.Alias ;
            parms[9].Value = model.TemplateType;
            parms[10].Value = model.IsShowFirstChildNode;
            parms[11].Value = model.BannerAdImageUrl == null ? string.Empty : model.BannerAdImageUrl;
            parms[12].Value = model.IsEnabled;
            parms[13].Value = model.IsDeleted;

            return Convert.ToInt32(Goodspeed.Library.Data.SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms));
        }
        #endregion

        #region == Update ==
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(CategoryInfo model)
        {
            string strSQL = "UPDATE Categories SET Name = @Name,Sort = @Sort,ImageUrl = @ImageUrl,LinkUrl = @LinkUrl,Introduction = @Introduction,Alias = @Alias,TemplateType = @TemplateType,IsShowFirstChildNode = @IsShowFirstChildNode,BannerAdImageUrl = @BannerAdImageUrl,IsEnabled = @IsEnabled,IsDeleted = @IsDeleted WHERE ID = @ID";
            SqlParameter[] parms = { 
                                    new SqlParameter("Language",SqlDbType.Int),
                                    new SqlParameter("ParentId",SqlDbType.Int),
                                    new SqlParameter("ParentIdList",SqlDbType.NVarChar),
                                    new SqlParameter("Sort",SqlDbType.Int),
                                    new SqlParameter("Name",SqlDbType.NVarChar),
                                    new SqlParameter("ImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("LinkUrl",SqlDbType.NVarChar),
                                    new SqlParameter("Introduction",SqlDbType.NVarChar),
                                    new SqlParameter("Alias",SqlDbType.NVarChar),
                                    new SqlParameter("TemplateType",SqlDbType.Int),
                                    new SqlParameter("IsShowFirstChildNode",SqlDbType.Int),
                                    new SqlParameter("BannerAdImageUrl",SqlDbType.NVarChar),
                                    new SqlParameter("IsEnabled",SqlDbType.Int),
                                    new SqlParameter("IsDeleted",SqlDbType.Int),
                                    new SqlParameter("Id",SqlDbType.Int),
                                   };
            parms[0].Value = (int)model.Language;
            parms[1].Value = model.ParentId;
            parms[2].Value = model.ParentIdList;
            parms[3].Value = model.Sort;
            parms[4].Value = model.Name == null ? string.Empty : model.Name;
            parms[5].Value = model.ImageUrl == null ? string.Empty : model.ImageUrl;
            parms[6].Value = model.LinkUrl == null ? string.Empty : model.LinkUrl;
            parms[7].Value = model.Introduction == null ? string.Empty : model.Introduction;
            parms[8].Value = model.Alias == null ? string.Empty : model.Alias;
            parms[9].Value = model.TemplateType;
            parms[10].Value = model.IsShowFirstChildNode;
            parms[11].Value = model.BannerAdImageUrl == null ? string.Empty : model.BannerAdImageUrl;
            parms[12].Value = model.IsEnabled;
            parms[13].Value = model.IsDeleted;
            parms[14].Value = model.Id;
            return Goodspeed.Library.Data.SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL,parms);
        }
        #endregion

        #region == 判断当前类别下名称是否重复 ==
        /// <summary>
        /// 判断当前类别下名称是否重复
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static bool ExistsName(int id,string name,int parentId,WebLanguage lang) { 
            //id == 0 说明添加，反之，编辑
            string strSQL = string.Empty;
            if (id == 0)
            {
                strSQL = "SELECT COUNT(*) FROM Categories WITH(NOLOCK) WHERE ParentId = @ParentId AND Name = @Name AND Language = @Language";
            }
            else {
                strSQL = "SELECT COUNT(*) FROM Categories WITH(NOLOCK) WHERE ParentId = @ParentId AND Name = @Name AND ID <> @ID AND Language = @Language";
            }
            SqlParameter[] parms = { 
                                    new SqlParameter("@ID",SqlDbType.Int),
                                    new SqlParameter("@Name",SqlDbType.VarChar),
                                    new SqlParameter("@ParentID",SqlDbType.Int),
                                    new SqlParameter("@Language",SqlDbType.Int)
                                   };
            parms[0].Value = id;
            parms[1].Value = name;
            parms[2].Value = parentId;
            parms[3].Value = (int)lang;

            return Convert.ToInt32(Goodspeed.Library.Data.SQLPlus.ExecuteScalar(CommandType.Text,strSQL,parms)) >0;
        }
        #endregion

        #region == 别名是否存在 ==
        public static bool ExistsAlias(int cid,string alias,WebLanguage lang) {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(*) FROM Categories WITH(NOLOCK)");
            sb.AppendFormat("   WHERE Language = {0}   ",(int)lang);
            sb.AppendFormat("   AND Alias = '{0}'    ", alias);
            if(cid != 0){
                sb.AppendFormat("   AND Id <> {0}   ",cid);
            }
            return Convert.ToInt32(Goodspeed.Library.Data.SQLPlus.ExecuteScalar(CommandType.Text,sb.ToString())) > 0;
        }
        #endregion

        #region == 获得分类详细信息 ==
        /// <summary>
        /// 获得详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CategoryInfo Get(int id)
        {
            string strSQL = string.Format("SELECT * FROM Categories WITH(NOLOCK) WHERE Id = {0}",id);
            return Get(Goodspeed.Library.Data.SQLPlus.ExecuteDataRow(CommandType.Text,strSQL));
        }
        private static CategoryInfo Get(DataRow dr)
        {
            CategoryInfo model = new CategoryInfo();
            if(dr != null){
                model.Alias = dr.Field<string>("Alias");
                model.BannerAdImageUrl = dr.Field<string>("BannerAdImageUrl");
                model.CreateDateTime = dr.Field<DateTime>("CreateDateTime");
                model.Id = dr.Field<int>("Id");
                model.ImageUrl = dr.Field<string>("ImageUrl");
                model.Introduction = dr.Field<string>("Introduction");
                model.IsDeleted = dr.Field<bool>("IsDeleted");
                model.IsEnabled = dr.Field<bool>("IsEnabled");
                model.IsShowFirstChildNode = dr.Field<bool>("IsShowFirstChildNode");
                model.Language = dr.Field<WebLanguage>("Language");
                model.LinkUrl = dr.Field<string>("LinkUrl");
                model.Name = dr.Field<string>("Name");
                model.ParentId = dr.Field<int>("ParentId");
                model.ParentIdList = dr.Field<string>("ParentIdList");
                model.Sort = dr.Field<int>("Sort");
                model.TemplateType = dr.Field<Int16>("TemplateType");
            }
            return model;
        }
        #endregion

        #region == 删除分类 ==
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void Delete(int id) {
            //现在不真正删除了，所以下面的代码就没作用了
            //首先判断此分类下是否有子分类
            //int count = ListByParentId(id).Count;
            //if(count >0){
            //    return false;
            //}

            string strSQL = string.Format("UPDATE Categories SET IsDeleted = 1 WHERE Id = {0}",id);
            Goodspeed.Library.Data.SQLPlus.ExecuteNonQuery(CommandType.Text,strSQL);
        }
        #endregion

        #region == 还原分类 ==
        public static void Restore(int id) {
            string strSQL = string.Format("UPDATE Categories SET IsDeleted = 0 WHERE Id = {0}", id);
            Goodspeed.Library.Data.SQLPlus.ExecuteNonQuery(CommandType.Text, strSQL);
        }
        #endregion

        #region == 根据语言获得所有分类 ==
        /// <summary>
        /// 根据语言获得所有分类
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static IList<CategoryInfo> ListByLanguage(WebLanguage lang) {
            string strSQL = string.Format("SELECT * FROM Categories WITH(NOLOCK) WHERE Language = {0} ORDER BY Sort",(int)lang);
            var list = new List<CategoryInfo>();
            CategoryInfo model = null;
            DataTable dt = Goodspeed.Library.Data.SQLPlus.ExecuteDataTable(CommandType.Text,strSQL);
            if(dt != null && dt.Rows.Count >0){
                foreach(DataRow dr in dt.Rows){
                    model = Get(dr);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region == 根据ParentId获得对应下的一级子分类==
        /// <summary>
        /// 根据ParentId获得对应下的一级子分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static IList<CategoryInfo> ListByParentId(int parentId) {
            string strSQL = string.Format("SELECT * FROM Categories WITH(NOLOCK) WHERE ParentId = {0} ORDER BY Sort",parentId);
            IList<CategoryInfo> list = new List<CategoryInfo>();
            CategoryInfo model = null;
            DataTable dt = Goodspeed.Library.Data.SQLPlus.ExecuteDataTable(CommandType.Text,strSQL);
            if(dt != null && dt.Rows.Count >0){
                foreach(DataRow dr in dt.Rows){
                    model = Get(dr);
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion 
    }
}
