using System.ComponentModel;
namespace BibliotecaSys.Domain.Enums;

/// <summary>
///     Messages Repository
/// </summary>
/// 
public enum ApplicationStatusCode
{
    // General code messages
    #region General

    /// <summary>
    ///     This means that the codex doesn't have the cause of the error
    /// </summary>
    [Description("This is an unexpected error or the codex doesn't have the cause of this error. Verify the logs for more information.")]
    Unknown = 0,
    [Description("The operation was successfully executed.")]
    Ok = 1,
    [Description("Found")] 
    Found = 5,
    [Description("Not found")] 
    NotFound = 6,
    [Description("Created successfully")] 
    Created = 7,
    [Description("Added successfully")] 
    Added = 8,
    [Description("Updated successfully")] 
    Updated = 9,
    [Description("Deleted successfully")] 
    Deleted = 10,
    [Description("Removed successfully")]
    Removed = 11,
    [Description("Wasn't succesfully created")]
    NotCreated = 12,
    [Description("Wasn't succesfully added")]
    NotAdded = 13,
    [Description("Wasn't succesfully updated")]
    NotUpdated = 14,
    [Description("Wasn't succesfully deleted")]
    NotDeleted = 15,
    [Description("Wasn't succesfully removed")]
    NotRemoved = 16,

    #endregion

    // Universal default codes for Web
    #region Web Global Defaults

    [Description("Not modified")] WebNotModified = 304,

    [Description("Bad request")] WebBadRequest = 400,

    [Description("Unauthorized")] WebUnauthorized = 401,

    [Description("Forbidden")] WebForbidden = 403,

    [Description("Not found")] WebNotFound = 404,

    [Description("Not acceptable")] WebNotAcceptable = 406,

    [Description("Not content")] WebNotContent = 204,

    [Description("Partial content")] WebPartialContent = 206,

    [Description("Timeout")] WebTimeout = 408,

    [Description("Conflict")] WebConflict = 409,

    [Description("Internal server error")] WebInternalServerError = 500,

    [Description("Unknown")] WebUnknown = 900,

    [Description("Soap error")] WebSoapError = 901,

    #endregion

    // Messages for user interaction
    #region User & Password Messages

    [Description("User login successfully")]
    UserLoginSuccess = 1001,

    [Description("User is disabled")] UserIsDisabled = 1002,

    [Description("Wrong user")] WrongUser = 1003,

    [Description("Wrong password")] WrongPassword = 1004,

    [Description("User was not provided")] UserNotProvided = 1005,

    [Description("Password was not provided")]
    PasswordNotProvided = 1006,

    [Description("User created successfully")]
    UserCreated = 1007,

    [Description("Password succesfully added")]
    PasswordAdded = 1008,

    [Description("User not found")] UserNotFound = 1009,

    [Description("User wasn't successfully created")]
    UserNotCreated = 1012,

    [Description("Password wasn't succesfully updated")]
    PasswordNotUpdated = 1014,

    [Description("Password wasn't succesfully removed")]
    PasswordNotRemoved = 1016,

    [Description("User signout successfully")]
    UserSignOut = 1027,

    [Description("User signout error")] UserSignOutError = 1028,

    #endregion

    // Message for role interaction
    #region Role Messages

    [Description("Role found")] RoleFound = 1105,

    [Description("Role not found")] RoleNotFound = 1106,

    [Description("Role created successfully")]
    RoleCreated = 1107,

    [Description("Role added successfully")]
    RoleAdded = 1108,

    [Description("Role updated successfully")]
    RoleUpdated = 1109,

    [Description("Role deleted successfully")]
    RoleDeleted = 1110,

    [Description("Role removed successfully")]
    RoleRemoved = 1111,

    [Description("Role wasn't succesfully created")]
    RoleNotCreated = 1112,

    [Description("Role wasn't succesfully added")]
    RoleNotAdded = 1113,

    [Description("Role wasn't succesfully updated")]
    RoleNotUpdated = 1114,

    [Description("Role wasn't succesfully deleted")]
    RoleNotDeleted = 1115,

    [Description("Role wasn't succesfully removed")]
    RoleNotRemoved = 1116,

    [Description("User doesn't have this role.")] RoleInUserNotFound = 1125,

    #endregion

    // Messages for claims interaction
    #region Claims Messages

    [Description("Claim added successfully")]
    ClaimAdded = 1117,

    [Description("Claim updated successfully")]
    ClaimUpdated = 1118,

    [Description("Claim deleted from user successfully")]
    ClaimDeletedFromUser = 1119,

    [Description("Claim removed successfully")]
    ClaimRemoved = 1120,

    [Description("Claim wasn't succesfully added")]
    ClaimNotAdded = 1121,

    [Description("Claim wasn't succesfully updated")]
    ClaimNotUpdated = 1122,

    [Description("Claim wasn't succesfully deleted")]
    ClaimNotDeleted = 1123,

    [Description("Claim wasn't succesfully removed")]
    ClaimNotRemoved = 1124,

    #endregion


    #region Others Messages
    [Description("An invalid id was provided")]
    InvalidId = 10000,

    [Description("An invalid pass was provided")]
    InvalidPass = 10001

    #endregion
}