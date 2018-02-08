using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chakad.Core;
using Chakad.Pipeline.Core.Exceptions;

namespace Chakad.Pipeline.Core.Options
{
    public class ValidationResultViewModel
    {
        public ValidationResultViewModel(bool isValid, string content)
        {
            IsValid = isValid;
            Content = content;
        }
        public bool IsValid { get; }
        public string Content { get; }
    }
    public class ChakadMessagePropertyConfig<T>
    {
        public ChakadMessagePropertyConfig(ChakadFieldType fieldType,
            T value,
            string label = "",
            bool isRequired = false)
        {
            LabelTitle = label;
            Value = value;
            FieldType = fieldType;
            IsRequired = isRequired;
        }
        public Func<object, ValidationResultViewModel> ValiationPolicy { get; set; }
        public string LabelTitle { get; }
        public bool IsRequired { get; }
        public ChakadFieldType FieldType { get; }
        public T Value { get; }
    }
  
    public class ChakadMessageProperty<T>
    {
        public Func<object, ValidationResultViewModel> ValiationPolicy { get; private set; }
        public ChakadFieldType FieldType { get; private set; }
        private ChakadMessageProperty()
        {

        }
        public static ChakadMessageProperty<T> Build(ChakadMessagePropertyConfig<T> config)
        {
            if (config == null)
                throw new ChakadObjectInitializationFieldException("Build ComponentPresenter was Field", config);

            var me = new ChakadMessageProperty<T>
            {
                IsRequired = config.IsRequired,
                ValiationPolicy = config.ValiationPolicy,
                FieldType = config.FieldType,
                Value = config.Value
            };

            if (!string.IsNullOrWhiteSpace(config.LabelTitle))
                me.Title = config.LabelTitle;

            return me;
        }
        #region Private Methods
        public void Reset()
        {
            Value = default(T);
        }
        #endregion
        #region Value
        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _value = value;
                }
            }
        }
        #endregion
        #region IsRequired
        public bool IsRequired { get; set; }
        #endregion
        #region Title
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        #endregion
    }
}
