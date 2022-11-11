/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface CreateUserDto {
  /**
   * ErrorCodes: UsernameEmpty, UsernameTooShort, UsernameTooLong, UsernameAlreadyExists
   * @minLength 0
   * @maxLength 32
   */
  username?: string
  /**
   * ErrorCodes: EmailEmpty, EmailInvalid, EmailAlreadyExists, EmailTooLong
   * @minLength 0
   * @maxLength 256
   */
  email?: string
  /**
   * ErrorCodes: PhoneNumberInvalid
   * @pattern ^\+?[1-9][0-9]{7,14}$
   */
  phoneNumber?: string | null
}

export type EmptyResponse = object

/** Wrapper used for returning failed responses. */
export interface ErrorResponse {
  /** List of errors. */
  errors?: TatuazError[]
  /** Indicates if request was successful. Should be always false for this type of response. */
  success?: boolean
}

export interface ListArtistStatsDto {
  /**
   * ErrorCodes: CountEmpty, CountTooLow, CountTooHigh
   * @format int32
   * @minLength 1
   */
  count?: number
}

export interface ListSummaryStatsDto {
  timePeriod?: SummaryStatTimePeriod
  /**
   * ErrorCodes: CountEmpty, CountTooLow, CountTooHigh
   * @format int32
   * @minLength 1
   */
  count?: number
}

/** Wrapper used for returning success responses. */
export interface OkResponseIEnumerableSummaryStatDto {
  /** Payload of response. */
  value?: SummaryStatDto[] | null
  /** Indicates if request was successful. Should be always true for this type of response. */
  success?: boolean
}

/** Wrapper used for returning success responses. */
export interface OkResponseUserDto {
  value?: UserDto
  /** Indicates if request was successful. Should be always true for this type of response. */
  success?: boolean
}

export interface SummaryStatDto {
  title?: string
  content?: string
  backgroundUrl?: string
}

export enum SummaryStatTimePeriod {
  Day = "Day",
  Week = "Week",
  Month = "Month",
}

export interface TatuazError {
  code?: string
  message?: string
}

export interface UserDto {
  username?: string
  email?: string
}

export type QueryParamsType = Record<string | number, any>
export type ResponseFormat = keyof Omit<Body, "body" | "bodyUsed">

export interface FullRequestParams extends Omit<RequestInit, "body"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean
  /** request path */
  path: string
  /** content type of request body */
  type?: ContentType
  /** query params */
  query?: QueryParamsType
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat
  /** request body */
  body?: unknown
  /** base url */
  baseUrl?: string
  /** request cancellation token */
  cancelToken?: CancelToken
}

export type RequestParams = Omit<
  FullRequestParams,
  "body" | "method" | "query" | "path"
>

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string
  baseApiParams?: Omit<RequestParams, "baseUrl" | "cancelToken" | "signal">
  securityWorker?: (
    securityData: SecurityDataType | null
  ) => Promise<RequestParams | void> | RequestParams | void
  customFetch?: typeof fetch
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown>
  extends Response {
  data: D
  error: E
}

type CancelToken = Symbol | string | number

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = ""
  private securityData: SecurityDataType | null = null
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"]
  private abortControllers = new Map<CancelToken, AbortController>()
  private customFetch = (...fetchParams: Parameters<typeof fetch>) =>
    fetch(...fetchParams)

  private baseApiParams: RequestParams = {
    credentials: "same-origin",
    headers: {},
    redirect: "follow",
    referrerPolicy: "no-referrer",
  }

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig)
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data
  }

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key)
    return `${encodedKey}=${encodeURIComponent(
      typeof value === "number" ? value : `${value}`
    )}`
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key])
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key]
    return value.map((v: any) => this.encodeQueryParam(key, v)).join("&")
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {}
    const keys = Object.keys(query).filter(
      (key) => "undefined" !== typeof query[key]
    )
    return keys
      .map((key) =>
        Array.isArray(query[key])
          ? this.addArrayQueryParam(query, key)
          : this.addQueryParam(query, key)
      )
      .join("&")
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery)
    return queryString ? `?${queryString}` : ""
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string")
        ? JSON.stringify(input)
        : input,
    [ContentType.FormData]: (input: any) =>
      Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key]
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === "object" && property !== null
            ? JSON.stringify(property)
            : `${property}`
        )
        return formData
      }, new FormData()),
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  }

  protected mergeRequestParams(
    params1: RequestParams,
    params2?: RequestParams
  ): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    }
  }

  protected createAbortSignal = (
    cancelToken: CancelToken
  ): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken)
      if (abortController) {
        return abortController.signal
      }
      return void 0
    }

    const abortController = new AbortController()
    this.abortControllers.set(cancelToken, abortController)
    return abortController.signal
  }

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken)

    if (abortController) {
      abortController.abort()
      this.abortControllers.delete(cancelToken)
    }
  }

  public request = async <T = any, E = any>({
    body,
    secure,
    path,
    type,
    query,
    format,
    baseUrl,
    cancelToken,
    ...params
  }: FullRequestParams): Promise<T> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {}
    const requestParams = this.mergeRequestParams(params, secureParams)
    const queryString = query && this.toQueryString(query)
    const payloadFormatter = this.contentFormatters[type || ContentType.Json]
    const responseFormat = format || requestParams.format

    return this.customFetch(
      `${baseUrl || this.baseUrl || ""}${path}${
        queryString ? `?${queryString}` : ""
      }`,
      {
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type && type !== ContentType.FormData
            ? { "Content-Type": type }
            : {}),
        },
        signal: cancelToken
          ? this.createAbortSignal(cancelToken)
          : requestParams.signal,
        body:
          typeof body === "undefined" || body === null
            ? null
            : payloadFormatter(body),
      }
    ).then(async (response) => {
      const r = response as HttpResponse<T, E>
      r.data = null as unknown as T
      r.error = null as unknown as E

      const data = !responseFormat
        ? r
        : await response[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data
              } else {
                r.error = data
              }
              return r
            })
            .catch((e) => {
              r.error = e
              return r
            })

      if (cancelToken) {
        this.abortControllers.delete(cancelToken)
      }

      if (!response.ok) throw data
      return data.data
    })
  }
}

/**
 * @title tatuaz.app API
 * @version v1
 *
 * API for tatuaz.app
 */
export class Api<
  SecurityDataType extends unknown
> extends HttpClient<SecurityDataType> {
  landing = {
    /**
     * No description
     *
     * @tags Landing
     * @name ListSummaryStatsCreate
     * @request POST:/Landing/ListSummaryStats
     * @secure
     * @response `200` `OkResponseIEnumerableSummaryStatDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `500` `ErrorResponse` Server Error
     */
    listSummaryStatsCreate: (
      data: ListSummaryStatsDto,
      params: RequestParams = {}
    ) =>
      this.request<OkResponseIEnumerableSummaryStatDto, ErrorResponse>({
        path: `/Landing/ListSummaryStats`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Landing
     * @name ListArtistStatsCreate
     * @request POST:/Landing/ListArtistStats
     * @secure
     * @response `200` `OkResponseIEnumerableSummaryStatDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `500` `ErrorResponse` Server Error
     */
    listArtistStatsCreate: (
      data: ListArtistStatsDto,
      params: RequestParams = {}
    ) =>
      this.request<OkResponseIEnumerableSummaryStatDto, ErrorResponse>({
        path: `/Landing/ListArtistStats`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
  users = {
    /**
     * No description
     *
     * @tags Users
     * @name WhoAmICreate
     * @summary Check what user is logged in
     * @request POST:/Users/WhoAmI
     * @secure
     * @response `200` `OkResponseUserDto` Success
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    whoAmICreate: (params: RequestParams = {}) =>
      this.request<OkResponseUserDto, EmptyResponse | ErrorResponse>({
        path: `/Users/WhoAmI`,
        method: "POST",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name SignUpCreate
     * @summary Register user
     * @request POST:/Users/SignUp
     * @secure
     * @response `201` `OkResponseUserDto` Created
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    signUpCreate: (data: CreateUserDto, params: RequestParams = {}) =>
      this.request<OkResponseUserDto, ErrorResponse | EmptyResponse>({
        path: `/Users/SignUp`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
}
