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

export interface BriefArtistDto {
  username: string
  /** @format uri */
  foregroundPhotoUri: string | null
  bio: string | null
  city: string | null
}

export interface CategoryDto {
  /** @format int32 */
  id: number
  title: string
  type: CategoryTypeDto
  imageUri: string
}

export enum CategoryTypeDto {
  Style = "Style",
  Motive = "Motive",
  BodyPart = "BodyPart",
}

export type DeleteBackgroundPhotoDto = object

export type DeleteForegroundPhotoDto = object

/** Response for marking codes that do not return any data. */
export type EmptyResponse = object

/** Wrapper used for returning failed responses. */
export interface ErrorResponse {
  /** List of errors. */
  errors: TatuazError[]
  /** Indicates if request was successful. Should be always false for this type of response. */
  success: boolean
}

export interface FinalizePostDto {
  /**
   * ErrorCodes
   * @format uuid
   */
  initialPostId: string
  /**
   * ErrorCodes: DescriptionIsNull, DescriptionIsTooLong
   * @maxLength 4096
   */
  description: string
  /** ErrorCodes: PhotoInfoDtosIsNull, PhotoInfoDtosTooMany, PhotoInfoDtosHasDuplicateCategoryIds, PhotoInfoDtosHasInvalidCategoryIds */
  photoInfoDtos: PhotoInfoDto[]
}

export interface GetTopArtistsDto {
  /**
   * ErrorCodes: PageNumberIsNull, PageNumberIsLessThan1
   * @format int32
   * @min 1
   */
  pageNumber: number
  /**
   * ErrorCodes: PageSizeIsNull, PageSizeIsLessThan1, PageSizeIsGreaterThan1000
   * @format int32
   * @min 1
   * @max 1000
   */
  pageSize: number
}

export interface GetUserDto {
  /**
   * ErrorCodes: UsernameNull, UsernameTooLong
   * @maxLength 32
   */
  username: string
}

export interface ListCategoriesDto {
  /**
   * ErrorCodes: PageNumberIsNull, PageNumberIsLessThan1
   * @format int32
   * @min 1
   */
  pageNumber: number
  /**
   * ErrorCodes: PageSizeIsNull, PageSizeIsLessThan1, PageSizeIsGreaterThan1000
   * @format int32
   * @min 1
   * @max 1000
   */
  pageSize: number
}

/** Wrapper used for returning success responses. */
export interface OkResponseEmptyResponse {
  /** Response for marking codes that do not return any data. */
  value: EmptyResponse
  /** Indicates if request was successful. Should be always true for this type of response. */
  success: boolean
}

/** Wrapper used for returning success responses. */
export interface OkResponsePagedDataBriefArtistDto {
  value: PagedDataBriefArtistDto
  /** Indicates if request was successful. Should be always true for this type of response. */
  success: boolean
}

/** Wrapper used for returning success responses. */
export interface OkResponsePagedDataCategoryDto {
  value: PagedDataCategoryDto
  /** Indicates if request was successful. Should be always true for this type of response. */
  success: boolean
}

/** Wrapper used for returning success responses. */
export interface OkResponseRegisteredStatsDto {
  value: RegisteredStatsDto
  /** Indicates if request was successful. Should be always true for this type of response. */
  success: boolean
}

/** Wrapper used for returning success responses. */
export interface OkResponseUploadedPhotosDto {
  value: UploadedPhotosDto
  /** Indicates if request was successful. Should be always true for this type of response. */
  success: boolean
}

/** Wrapper used for returning success responses. */
export interface OkResponseUserDto {
  value: UserDto
  /** Indicates if request was successful. Should be always true for this type of response. */
  success: boolean
}

export interface PagedDataBriefArtistDto {
  data: BriefArtistDto[]
  /** @format int32 */
  pageNumber: number
  /** @format int32 */
  pageSize: number
  /** @format int32 */
  totalPages: number
  /** @format int32 */
  totalCount: number
}

export interface PagedDataCategoryDto {
  data: CategoryDto[]
  /** @format int32 */
  pageNumber: number
  /** @format int32 */
  pageSize: number
  /** @format int32 */
  totalPages: number
  /** @format int32 */
  totalCount: number
}

export interface PhotoInfoDto {
  /** @format uuid */
  photoId: string
  categoryIds: number[]
  photoFileName: string
}

export interface RegisteredStatsDto {
  /** @format int32 */
  artists: number
  /** @format int32 */
  clients: number
  /** @format int32 */
  users: number
}

export interface SetAccountTypeDto {
  /** ErrorCodes: ArtistNull */
  artist: boolean
}

export interface SetBioDto {
  /**
   * ErrorCodes: BioTooLong
   * @maxLength 4096
   */
  bio: string | null
  /**
   * ErrorCodes: CityTooLong
   * @maxLength 64
   */
  city: string | null
}

export interface SignUpDto {
  /**
   * ErrorCodes: UsernameNull, UsernameTooShort, UsernameTooLong, UsernameAlreadyInUse, UsernameInvalidCharacters
   * @minLength 4
   * @maxLength 32
   * @pattern ^[a-zA-Z0-9_]*$
   */
  username: string
  /** ErrorCodes: CategoryIdsNull, CategoryIdsTooFew, CategoryIdsInvalid, CategoryIdsDuplicate */
  categoryIds: number[]
}

export interface TatuazError {
  code: string
  message: string
}

export interface UploadedPhotosDto {
  /** @format uuid */
  initialPostId: string
  photos: string[]
}

export interface UserDto {
  username: string
  email: string
  auth0Id: string
  /** @format uri */
  foregroundPhotoUri: string | null
  /** @format uri */
  backgroundPhotoUri: string | null
  bio: string | null
  city: string | null
  artist: boolean
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

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string
  baseApiParams?: Omit<RequestParams, "baseUrl" | "cancelToken" | "signal">
  securityWorker?: (securityData: SecurityDataType | null) => Promise<RequestParams | void> | RequestParams | void
  customFetch?: typeof fetch
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown> extends Response {
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
  private customFetch = (...fetchParams: Parameters<typeof fetch>) => fetch(...fetchParams)

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
    return `${encodedKey}=${encodeURIComponent(typeof value === "number" ? value : `${value}`)}`
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
    const keys = Object.keys(query).filter((key) => "undefined" !== typeof query[key])
    return keys
      .map((key) => (Array.isArray(query[key]) ? this.addArrayQueryParam(query, key) : this.addQueryParam(query, key)))
      .join("&")
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery)
    return queryString ? `?${queryString}` : ""
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string") ? JSON.stringify(input) : input,
    [ContentType.FormData]: (input: any) =>
      Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key]
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === "object" && property !== null
            ? JSON.stringify(property)
            : `${property}`,
        )
        return formData
      }, new FormData()),
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  }

  protected mergeRequestParams(params1: RequestParams, params2?: RequestParams): RequestParams {
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

  protected createAbortSignal = (cancelToken: CancelToken): AbortSignal | undefined => {
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

    return this.customFetch(`${baseUrl || this.baseUrl || ""}${path}${queryString ? `?${queryString}` : ""}`, {
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type && type !== ContentType.FormData ? { "Content-Type": type } : {}),
      },
      signal: cancelToken ? this.createAbortSignal(cancelToken) : requestParams.signal,
      body: typeof body === "undefined" || body === null ? null : payloadFormatter(body),
    }).then(async (response) => {
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
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  identity = {
    /**
     * No description
     *
     * @tags Identity
     * @name Me
     * @summary Check what user is logged in
     * @request POST:/Identity/Me
     * @secure
     * @response `200` `OkResponseUserDto` Success
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    me: (params: RequestParams = {}) =>
      this.request<OkResponseUserDto, EmptyResponse | ErrorResponse>({
        path: `/Identity/Me`,
        method: "POST",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Identity
     * @name SignUp
     * @summary Register user
     * @request POST:/Identity/SignUp
     * @secure
     * @response `201` `OkResponseUserDto` Created
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    signUp: (data: SignUpDto, params: RequestParams = {}) =>
      this.request<OkResponseUserDto, ErrorResponse | EmptyResponse>({
        path: `/Identity/SignUp`,
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
     * @tags Identity
     * @name SetForegroundPhoto
     * @summary Set foreground photo
     * @request POST:/Identity/SetForegroundPhoto
     * @secure
     * @response `201` `EmptyResponse` Created
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    setForegroundPhoto: (
      data: {
        /** @format binary */
        photo?: File
      },
      params: RequestParams = {},
    ) =>
      this.request<EmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Identity/SetForegroundPhoto`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.FormData,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Identity
     * @name SetBackgroundPhoto
     * @summary Set background photo
     * @request POST:/Identity/SetBackgroundPhoto
     * @secure
     * @response `201` `EmptyResponse` Created
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    setBackgroundPhoto: (
      data: {
        /** @format binary */
        photo?: File
      },
      params: RequestParams = {},
    ) =>
      this.request<EmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Identity/SetBackgroundPhoto`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.FormData,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Identity
     * @name DeleteForegroundPhoto
     * @summary Delete foreground photo
     * @request POST:/Identity/DeleteForegroundPhoto
     * @secure
     * @response `201` `EmptyResponse` Created
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    deleteForegroundPhoto: (data: DeleteForegroundPhotoDto, params: RequestParams = {}) =>
      this.request<EmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Identity/DeleteForegroundPhoto`,
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
     * @tags Identity
     * @name DeleteBackgroundPhoto
     * @summary Delete background photo
     * @request POST:/Identity/DeleteBackgroundPhoto
     * @secure
     * @response `201` `EmptyResponse` Created
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    deleteBackgroundPhoto: (data: DeleteBackgroundPhotoDto, params: RequestParams = {}) =>
      this.request<EmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Identity/DeleteBackgroundPhoto`,
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
     * @tags Identity
     * @name GetUser
     * @summary Get user with username
     * @request POST:/Identity/GetUser
     * @secure
     * @response `201` `OkResponseUserDto` Created
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    getUser: (data: GetUserDto, params: RequestParams = {}) =>
      this.request<OkResponseUserDto, ErrorResponse | EmptyResponse>({
        path: `/Identity/GetUser`,
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
     * @tags Identity
     * @name SetBio
     * @summary Set bio for current user
     * @request POST:/Identity/SetBio
     * @secure
     * @response `200` `EmptyResponse` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    setBio: (data: SetBioDto, params: RequestParams = {}) =>
      this.request<EmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Identity/SetBio`,
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
     * @tags Identity
     * @name SetAccountType
     * @summary Set account type for current user
     * @request POST:/Identity/SetAccountType
     * @secure
     * @response `200` `EmptyResponse` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    setAccountType: (data: SetAccountTypeDto, params: RequestParams = {}) =>
      this.request<EmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Identity/SetAccountType`,
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
     * @tags Identity
     * @name GetTopArtists
     * @summary Set account type for current user
     * @request POST:/Identity/GetTopArtists
     * @secure
     * @response `200` `OkResponsePagedDataBriefArtistDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    getTopArtists: (data: GetTopArtistsDto, params: RequestParams = {}) =>
      this.request<OkResponsePagedDataBriefArtistDto, ErrorResponse | EmptyResponse>({
        path: `/Identity/GetTopArtists`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
  photo = {
    /**
     * No description
     *
     * @tags Photo
     * @name ListCategories
     * @request POST:/Photo/ListCategories
     * @secure
     * @response `200` `OkResponsePagedDataCategoryDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    listCategories: (data: ListCategoriesDto, params: RequestParams = {}) =>
      this.request<OkResponsePagedDataCategoryDto, ErrorResponse | EmptyResponse>({
        path: `/Photo/ListCategories`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
  post = {
    /**
     * No description
     *
     * @tags Post
     * @name UploadPostPhotos
     * @summary Upload post photos
     * @request POST:/Post/UploadPostPhotos
     * @secure
     * @response `200` `OkResponseUploadedPhotosDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    uploadPostPhotos: (
      data: {
        Photos?: File[]
      },
      params: RequestParams = {},
    ) =>
      this.request<OkResponseUploadedPhotosDto, ErrorResponse | EmptyResponse>({
        path: `/Post/UploadPostPhotos`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.FormData,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Post
     * @name FinalizePost
     * @summary Finalize post
     * @request POST:/Post/FinalizePost
     * @secure
     * @response `200` `OkResponseEmptyResponse` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    finalizePost: (data: FinalizePostDto, params: RequestParams = {}) =>
      this.request<OkResponseEmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Post/FinalizePost`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
  statistics = {
    /**
 * No description
 *
 * @tags Statistics
 * @name GetRegisteredStats
 * @summary Get number of registered tattoo artists, clients and users.
A client is a user who has booked at least 1 appointment
 * @request POST:/Statistics/GetRegisteredStats
 * @secure
 * @response `200` `OkResponseRegisteredStatsDto` Success
 * @response `500` `ErrorResponse` Server Error
 */
    getRegisteredStats: (params: RequestParams = {}) =>
      this.request<OkResponseRegisteredStatsDto, ErrorResponse>({
        path: `/Statistics/GetRegisteredStats`,
        method: "POST",
        secure: true,
        format: "json",
        ...params,
      }),
  }
}
