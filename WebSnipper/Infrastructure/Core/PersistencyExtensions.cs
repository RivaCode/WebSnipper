namespace Infrastructure.Core
{
    internal static class PersistencyExtensions
    {
        public static TPersistence AsPersistence<TModel, TPersistence>(
            this TModel src,
            IModelConverter<TModel, TPersistence> converter)
            where TPersistence : PersistencyObject 
            => converter.ToPersisted(src);

        public static TModel AsModel<TModel, TPersistence>(
            this TPersistence src,
            IModelConverter<TModel, TPersistence> converter)
            where TPersistence : PersistencyObject
            => converter.ToModel(src);
    }
}