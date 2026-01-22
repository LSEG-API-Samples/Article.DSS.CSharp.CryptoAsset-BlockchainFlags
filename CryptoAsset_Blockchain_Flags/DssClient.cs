using DataScope.Select.Api.Content;
using DataScope.Select.Api.Extractions;
using DataScope.Select.Api.Extractions.ExtractionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS_CryptoAsset_Blockchain_Flags
{
    class DssClient
    {
        private ExtractionsContext extractionsContext;

        private Uri dssUri = new Uri("https://selectapi.datascope.lseg.com/RestApi/v1/");

        public void ConnectToServer(string dssUserName, string dssUserPassword)
        {
            extractionsContext = new ExtractionsContext(dssUri, dssUserName, dssUserPassword);
        }

        public string SessionToken
        {
            //The session token is only generated if the server connection is successful.
            get { return extractionsContext.SessionToken; }
        }

        //Create and run a T&C on demand extraction. Retrieve JSON formatted data:
        public ExtractionResult CreateAndRunTandCExtraction(
            InstrumentIdentifier[] instrumentIdentifiers, string[] contentFieldNames)
        {
            TermsAndConditionsExtractionRequest extractionTandC = new TermsAndConditionsExtractionRequest
            {
                IdentifierList = InstrumentIdentifierList.Create(instrumentIdentifiers),
                ContentFieldNames = contentFieldNames,
                //Optional conditions:
                //Condition = new TermsAndConditionsCondition
                //{
                //    IssuerAssetClassType = IssuerAssetClassType.AllSupportedAssets,
                //    ExcludeWarrants = false,
                //    //DaysAgo = 3, //Use either DaysAgo or StartDate
                //    StartDate = new DateTimeOffset(new DateTime(1996, 1, 1)),
                //    FixedIncomeRatingSources = FixedIncomeRatingSource.StandardAndPoors
                //}
            };
            //Run the extraction.
            //This call is blocking, it returns when the extraction is completed:
            return extractionsContext.ExtractWithNotes(extractionTandC);
        }

        //Create and run a T&C on demand extraction. Retrieve a Gzipped CSV:
        public RawExtractionResult CreateAndRunTandCRawExtraction(
            InstrumentIdentifier[] instrumentIdentifiers, string[] contentFieldNames)
        {
            TermsAndConditionsExtractionRequest extractionTandC = new TermsAndConditionsExtractionRequest
            {
                IdentifierList = InstrumentIdentifierList.Create(instrumentIdentifiers),
                ContentFieldNames = contentFieldNames,
                //Optional conditions:
                //Condition = new TermsAndConditionsCondition
                //{
                //    IssuerAssetClassType = IssuerAssetClassType.AllSupportedAssets,
                //    ExcludeWarrants = false,
                //    //DaysAgo = 3, //Use either DaysAgo or StartDate
                //    StartDate = new DateTimeOffset(new DateTime(1996, 1, 1)),
                //    FixedIncomeRatingSources = FixedIncomeRatingSource.StandardAndPoors
                //}
            };
            //Run the extraction.
            //This call is blocking, it returns when the extraction is completed:
            return extractionsContext.ExtractRaw(extractionTandC);
        }

    }
}

