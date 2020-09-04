using NoteSystem.BLL.Interfaces;
using System;

namespace NoteSystem.BLL
{
    public static class CreatableFactory
    {
        public static TCreatable Now<TCreatable>() where TCreatable : ICreatable, new()
        {
            return new TCreatable
            {
                Changed = DateTime.Now,
                Created = DateTime.Now
            };
        }

        public static TCreatable NowWith<TCreatable>(Action<TCreatable> builder) where TCreatable : ICreatable, new()
        {
            var result = Now<TCreatable>();

            builder.Invoke(result);

            return result;
        }
    }
}
