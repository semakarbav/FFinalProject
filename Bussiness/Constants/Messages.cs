using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bussiness.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoriError="Limit aşıldı";
        public static string ProductNameErrorAlreadyExists="Bu isimde zaten başka ürün var";
        public static string CategoryLimitExceded="Categori limiti aşıldiği için yeni ürün eklenemiyor";
        internal static string AuthorizationDenied="Yetkiniz Yok";
        internal static string UserRegistered="Kayıt Olundu";
        internal static string UserNotFound="Kullanıcı Bulunamadı";
        internal static string PasswordError="Şifre Yanlış";
        internal static string SuccessfulLogin="Başarılı Giriş";
        internal static string UserAlreadyExists="Kullanıcı Zaten Var";
        internal static string AccessTokenCreated="Token Oluşturuldu";
    }
}
