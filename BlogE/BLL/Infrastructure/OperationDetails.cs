using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    //Класс хранит информацию об успешности операции. Свойство Succedeed указывает,
    //успешна ли операция, а свойства Message и Property будут хранить  сообщение об
    //ошибке и свойство, на котором произошла ошибка.
    public class OperationDetails
    {

        public OperationDetails(bool success, string message, string prop)
        {
            Success = success;
            Message = message;
            Property = prop;
        }
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
    }
    //https://metanit.com/sharp/mvc5/23.11.php
}
