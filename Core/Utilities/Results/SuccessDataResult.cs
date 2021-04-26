using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message):base(data,true,message)//tüm halini kullanırsa
        {

        }
        public SuccessDataResult(T data) : base(data, true) //mesaj kullanılmayacksa,sadece data ver
        {

        }
        public SuccessDataResult(string message):base(default,true,message) //sadece mesaj
        {

        }
        public SuccessDataResult():base(default,true) //hiçbir şey verilmeyecekse
        {

        }
    }
}
