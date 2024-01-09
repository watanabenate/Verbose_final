﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verbose.API;
using Verbose.Data;
using Verbose.src.viewholders;

namespace Verbose.src.adapters
{
    internal class CommentCardAdapter : RecyclerView.Adapter
    {
        public override int ItemCount => commentList.Count;

        public List<Comment> commentList;
        VerboseAPIService _api;

        bool showTimestamp;

        public CommentCardAdapter(List<Comment> commentList, bool showT)
        {
            this.commentList = commentList;
            _api = VerboseAPIService.Instance;
            showTimestamp = showT;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.comment_card, parent, false);

            return new CommentViewHolder(itemView, LikeClick, ProfileClick);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CommentViewHolder vh = holder as CommentViewHolder;
            Bitmap b = _api.GetImageBitmap(commentList[position].ProfileImageLink);
            vh.ProfileImage.SetImageBitmap(b);
            vh.ProfileUsername.Text = commentList[position].Username;

            if (showTimestamp)
            {
                vh.TimeStamp.Text = _api.getTimeString(commentList[position].Timestamp);
            }
            else
            {
                vh.TimeStamp.Text = "";
            }
            
            vh.Body.Text = commentList[position].Text;
            vh.LikeCount.Text = commentList[position].Likes.ToString();
        }

        private void LikeClick(int position)
        {
            if (LikeClickHandler != null)
                LikeClickHandler(this, position);
        }

        public event EventHandler<int> LikeClickHandler;

        private void ProfileClick(int position)
        {
            if (ProfileClickHandler != null)
                ProfileClickHandler(this, position);
        }

        public event EventHandler<int> ProfileClickHandler;
    }
}

    