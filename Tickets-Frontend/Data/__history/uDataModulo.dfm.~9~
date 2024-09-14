object DataModulo: TDataModulo
  OnCreate = DataModuleCreate
  OnDestroy = DataModuleDestroy
  Height = 314
  Width = 347
  object FClient: TRESTClient
    BaseURL = 'http://localhost:5251'
    Params = <>
    RaiseExceptionOn500 = False
    ConnectTimeout = 5000
    SynchronizedEvents = False
    Left = 152
    Top = 56
  end
  object FRequest: TRESTRequest
    Client = FClient
    Params = <>
    Response = FResponse
    SynchronizedEvents = False
    Left = 112
    Top = 136
  end
  object FResponse: TRESTResponse
    Left = 248
    Top = 136
  end
end
