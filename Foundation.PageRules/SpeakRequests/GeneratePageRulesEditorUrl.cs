using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.ExperienceEditor.Speak.Server.Contexts;
using Sitecore.ExperienceEditor.Speak.Server.Requests;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Text;

namespace Foundation.PageRules.SpeakRequests
{
    public class GeneratePageRulesEditorUrl : PipelineProcessorRequest<ItemContext>
    {
        private string GenerateUrl() => new FieldEditorOptions((IEnumerable<FieldDescriptor>)this.CreateFieldDescriptors(this.RequestContext.Argument))
        {
            SaveItem = true
        }.ToUrlString().ToString();

        private List<FieldDescriptor> CreateFieldDescriptors(string fields)
        {
            List<FieldDescriptor> fieldDescriptorList = new List<FieldDescriptor>();
            foreach (string fieldName in new ListString(fields))
                fieldDescriptorList.Add(new FieldDescriptor(this.RequestContext.Item, fieldName));
            return fieldDescriptorList;
        }

        public override PipelineProcessorResponseValue ProcessRequest() => new PipelineProcessorResponseValue()
        {
            Value = (object)this.GenerateUrl()
        };
    }
}