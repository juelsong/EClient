const ErrorCode = {
  NoError: 0,
  User: {
    WrongPassword: 1001,
    NotExist: 1002,
    Forbidden: 1003,
    TokenExpired: 1004,
    NotAuthorized: 1005,
    CodeError: 1006,
    NotValidPassword: 1007,
    AlreadyExist: 1008
  },
  Service: {
    InnerError: 2001,
    NeedESign: 2002,
    DataExpired: 2003,
    DataNotExist: 2004,
    DeactiveInvalid: 2005,
    InvalidModel: 2006,
    DuplicateESign: 2007
  },
  Report: {
    TypeError: 3001,
    NotExist: 3002
  },
  Workflow: {
    VersionError: 4001,
    LackStartStep: 4002,
    LackStartStepPort: 4003,
    TooManyStartStep: 4004,
    LoadError: 4005
  },
  Approval: {
    Denied: 5001
  },
  Device: {
    ConfigError: 6001,
    ConnectError: 6002,
    GetDataError: 6003,
    SNError: 6004,
    DataError: 6005,
    NotSelectFile: 6006
  },
  Import: {
    TypeError: 7001,
    NotExist: 7002,
    DataError: 7003
  }
};

export default ErrorCode;

export function GetPromptKeyFromCode(code: number): string {
  switch (code) {
    case 0:
	    return 'ErrorPrompt.NoError';
    case 1001:
	    return 'ErrorPrompt.User.WrongPassword';
    case 1002:
	    return 'ErrorPrompt.User.NotExist';
    case 1003:
	    return 'ErrorPrompt.User.Forbidden';
    case 1004:
	    return 'ErrorPrompt.User.TokenExpired';
    case 1005:
	    return 'ErrorPrompt.User.NotAuthorized';
    case 1006:
	    return 'ErrorPrompt.User.CodeError';
    case 1007:
	    return 'ErrorPrompt.User.NotValidPassword';
    case 1008:
	    return 'ErrorPrompt.User.AlreadyExist';
    case 2001:
	    return 'ErrorPrompt.Service.InnerError';
    case 2002:
	    return 'ErrorPrompt.Service.NeedESign';
    case 2003:
	    return 'ErrorPrompt.Service.DataExpired';
    case 2004:
	    return 'ErrorPrompt.Service.DataNotExist';
    case 2005:
	    return 'ErrorPrompt.Service.DeactiveInvalid';
    case 2006:
	    return 'ErrorPrompt.Service.InvalidModel';
    case 2007:
	    return 'ErrorPrompt.Service.DuplicateESign';
    case 3001:
	    return 'ErrorPrompt.Report.TypeError';
    case 3002:
	    return 'ErrorPrompt.Report.NotExist';
    case 4001:
	    return 'ErrorPrompt.Workflow.VersionError';
    case 4002:
	    return 'ErrorPrompt.Workflow.LackStartStep';
    case 4003:
	    return 'ErrorPrompt.Workflow.LackStartStepPort';
    case 4004:
	    return 'ErrorPrompt.Workflow.TooManyStartStep';
    case 4005:
	    return 'ErrorPrompt.Workflow.LoadError';
    case 5001:
	    return 'ErrorPrompt.Approval.Denied';
    case 6001:
	    return 'ErrorPrompt.Device.ConfigError';
    case 6002:
	    return 'ErrorPrompt.Device.ConnectError';
    case 6003:
	    return 'ErrorPrompt.Device.GetDataError';
    case 6004:
	    return 'ErrorPrompt.Device.SNError';
    case 6005:
	    return 'ErrorPrompt.Device.DataError';
    case 6006:
	    return 'ErrorPrompt.Device.NotSelectFile';
    case 7001:
	    return 'ErrorPrompt.Import.TypeError';
    case 7002:
	    return 'ErrorPrompt.Import.NotExist';
    case 7003:
	    return 'ErrorPrompt.Import.DataError';
    default:
      throw "not implement in GetPromptKeyFromCode";
  }
}
