using System;


namespace GIatDo.ViewModel
{
    public class SlotVM
    {
        public Guid Id { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }
    public class SlotCM
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }

    public class SlotPriceVM
    {
        public object Key { get; set; }
        public object Value { get; set; }
    }
}
