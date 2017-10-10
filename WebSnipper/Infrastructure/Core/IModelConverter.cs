namespace Infrastructure.Core
{
    internal interface IModelConverter<TModel, TPersisted>
    {
        TModel ToModel(TPersisted persisted);
        TPersisted ToPersisted(TModel model);
        void SyncChanges(TModel from, TPersisted to);
    }
}