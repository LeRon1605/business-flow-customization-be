namespace Domain;

public static class ErrorCodes
{
    #region Space

    public const string SpaceAlreadyExisted = "Space:000001";
    public const string SpaceAlreadyCreatedBusinessFlow = "Space:000002";
    public const string SpaceNotFound = "Space:000003";

    #endregion
    
    #region SpaceMember
    
    public const string SpaceMemberAlreadyExisted = "SpaceMember:000001";
    
    #endregion

    #region BusinessFlow

    public const string InvalidBusinessFlowBranch = "BusinessFlow:000001";
    public const string InvalidBusinessFlow = "BusinessFlow:000002";
    public const string BusinessFlowNotFound = "BusinessFlow:000003";

    #endregion

    #region Submission

    public const string InvalidSubmissionFieldValue = "Submission:000001";
    public const string SubmissionFieldValueIsRequired = "Submission:000002";
    public const string SubmissionNotFound = "Submission:000003";

    #endregion

    #region Form

    public const string SpaceFormAlreadyExisted = "Form:000001";
    public const string InvalidFormElementSetting = "Form:000002";
    public const string FormNotFound = "Form:000003";
    public const string FormVersionNotFound = "Form:000004";
    public const string FormElementNotFound = "Form:000005";

    #endregion
}