using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HangManAssignment
{
    public class DataAdapter : BaseAdapter<CategoryList>
    {
        //Data adaptor for fillng in List view
        List<CategoryList> items;
        Activity context;

        public DataAdapter(Activity context, List<CategoryList> items)
            : base()
        {
            this.context = context;
            this.items = items;

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override CategoryList this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.layoutCategoryRow, null);

            view.FindViewById<TextView>(Resource.Id.Cat).Text = item.Title;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageInt);

            return view;

        }

    }
}