        public ActionResult HoursChart()
        {
            ChartContext context = new ChartContext();

            //Get the first month for the chart that is six months back from the current date
            DateTime startMonth = DateTime.Today.AddMonths(-6);

            var chartData = from o in context.tbl_Reports
                       .AsEnumerable()
                       where o.Date > startMonth
                       orderby o.Date ascending
                       group o by o.Date.Month into month
                       select new
                       {
                           Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Key),
                           Hours = month.Sum(i => i.Hours)
                       };

            return Json(chartData, JsonRequestBehavior.AllowGet);
        }