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

export interface BookingRequestDto {
  /** @format int32 */
  id: number
  artistName: string
  /** @format date-time */
  start: string
  /** @format date-time */
  end: string
  comment: string | null
  status: BookingRequestStatus
}

export enum BookingRequestStatus {
  Pending = "Pending",
  Accepted = "Accepted",
  Rejected = "Rejected",
}

export interface BriefPostDto {
  /** @format uuid */
  id: string
  description: string
  photoUris: string[]
  authorName: string
  /** @format uri */
  authorPhotoUri: string | null
  /** @format int32 */
  likesCount: number
  isLikedByCurrentUser: boolean
  /** @format int32 */
  commentsCount: number
  createdAt: Instant
}

export interface BriefUserDto {
  username: string
  /** @format uri */
  foregroundPhotoUri: string | null
  /** @format uri */
  backgroundPhotoUri: string | null
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

export interface GetPostFeedDto {
  /**
   * ErrorCodes: PageNumberIsNull, PageNumberIsLessThan1
   * @format int32
   * @min 1
   */
  pageNumber: number
  /**
   * ErrorCodes: PageSizeIsLessThan1, PageSizeIsGreaterThan1000
   * @format int32
   * @min 1
   * @max 1000
   */
  pageSize: number | null
  searchPostsFlag: SearchPostsFlag
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

export interface GetUserPostsDto {
  /** ErrorCodes: UsernameIsNull */
  username: string
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

export type Instant = object

export interface LikeCommentDto {
  /**
   * ErrorCodes: CommentIdIsNull
   * @format uuid
   */
  commentId: string
  /** ErrorCodes: LikeIsNull */
  like: boolean
}

export interface LikePostDto {
  /**
   * ErrorCodes: PostIdIsNull
   * @format uuid
   */
  postId: string
  /** ErrorCodes: LikeIsNull */
  like: boolean
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

export interface ListIncomingBookingRequestsDto {
  status: BookingRequestStatus
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

export interface ListMyBookingRequestsDto {
  status: BookingRequestStatus
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
export interface OkResponsePagedDataBriefPostDto {
  value: PagedDataBriefPostDto
  /** Indicates if request was successful. Should be always true for this type of response. */
  success: boolean
}

/** Wrapper used for returning success responses. */
export interface OkResponsePagedDataBriefUserDto {
  value: PagedDataBriefUserDto
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
export interface OkResponseSubmittedCommentDto {
  value: SubmittedCommentDto
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

export interface PagedDataBookingRequestDto {
  data: BookingRequestDto[]
  /** @format int32 */
  pageNumber: number
  /** @format int32 */
  pageSize: number
  /** @format int32 */
  totalPages: number
  /** @format int32 */
  totalCount: number
}

export interface PagedDataBriefPostDto {
  data: BriefPostDto[]
  /** @format int32 */
  pageNumber: number
  /** @format int32 */
  pageSize: number
  /** @format int32 */
  totalPages: number
  /** @format int32 */
  totalCount: number
}

export interface PagedDataBriefUserDto {
  data: BriefUserDto[]
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

export interface RespondToBookingRequestDto {
  /**
   * ErrorCodes: BookingRequestIdIsNull
   * @format int32
   */
  bookingRequestId: number
  /** ErrorCodes: AcceptIsNull */
  accept: boolean
}

export interface SearchPostsDto {
  /**
   * ErrorCodes: QueryNull, QueryTooLong
   * @maxLength 128
   */
  query: string
  /**
   * ErrorCodes: PageNumberIsNull, PageNumberIsLessThan1
   * @format int32
   * @min 1
   */
  pageNumber: number
  /**
   * ErrorCodes: PageSizeIsLessThan1, PageSizeIsGreaterThan1000
   * @format int32
   * @min 1
   * @max 1000
   */
  pageSize: number | null
  searchPostsFlag: SearchPostsFlag
}

export enum SearchPostsFlag {
  All = "All",
  OnlyPosts = "OnlyPosts",
  OnlyPhotos = "OnlyPhotos",
}

export interface SearchUsersDto {
  /** ErrorCodes: QueryNull */
  query: string
  /**
   * ErrorCodes: PageNumberLessThan1
   * @format int32
   * @min 1
   */
  pageNumber: number | null
  /**
   * ErrorCodes: PageSizeLessThan1, PageSizeGreaterThan1000
   * @format int32
   * @min 1
   * @max 1000
   */
  pageSize: number | null
  /** ErrorCodes: OnlyArtistsNull */
  onlyArtists: boolean
}

export interface SendBookingRequestDto {
  /** ErrorCodes: ArtistEmailIsNull */
  artistName: string
  /**
   * ErrorCodes: StartIsNull, StartIsGreaterThanEnd
   * @format date-time
   */
  start: string
  /**
   * ErrorCodes: EndIsNull
   * @format date-time
   */
  end: string
  /**
   * ErrorCodes: CommentIsTooLong
   * @maxLength 1024
   */
  comment: string | null
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

export interface SubmitCommentDto {
  /**
   * ErrorCodes: PostIsNull, PostNotFound, ParentCommentNotFound
   * @format uuid
   */
  postId: string
  /**
   * ErrorCodes: ParentCommentNotFound
   * @format uuid
   */
  parentCommentId: string | null
  /**
   * ErrorCodes: ContentIsNull, ContentIsTooLong
   * @maxLength 4096
   */
  content: string
}

export interface SubmittedCommentDto {
  /** @format uuid */
  commentId: string
  content: string
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

import axios, {
  AxiosInstance,
  AxiosRequestConfig,
  HeadersDefaults,
  ResponseType,
} from "axios"

export type QueryParamsType = Record<string | number, any>

export interface FullRequestParams
  extends Omit<AxiosRequestConfig, "data" | "params" | "url" | "responseType"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean
  /** request path */
  path: string
  /** content type of request body */
  type?: ContentType
  /** query params */
  query?: QueryParamsType
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType
  /** request body */
  body?: unknown
}

export type RequestParams = Omit<
  FullRequestParams,
  "body" | "method" | "query" | "path"
>

export interface ApiConfig<SecurityDataType = unknown>
  extends Omit<AxiosRequestConfig, "data" | "cancelToken"> {
  securityWorker?: (
    securityData: SecurityDataType | null
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void
  secure?: boolean
  format?: ResponseType
}

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance
  private securityData: SecurityDataType | null = null
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"]
  private secure?: boolean
  private format?: ResponseType

  constructor({
    securityWorker,
    secure,
    format,
    ...axiosConfig
  }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({
      ...axiosConfig,
      baseURL: axiosConfig.baseURL || "",
    })
    this.secure = secure
    this.format = format
    this.securityWorker = securityWorker
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data
  }

  protected mergeRequestParams(
    params1: AxiosRequestConfig,
    params2?: AxiosRequestConfig
  ): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method)

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method &&
          this.instance.defaults.headers[
            method.toLowerCase() as keyof HeadersDefaults
          ]) ||
          {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    }
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === "object" && formItem !== null) {
      return JSON.stringify(formItem)
    } else {
      return `${formItem}`
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key]
      const propertyContent: any[] =
        property instanceof Array ? property : [property]

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File
        formData.append(
          key,
          isFileType ? formItem : this.stringifyFormItem(formItem)
        )
      }

      return formData
    }, new FormData())
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<T> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {}
    const requestParams = this.mergeRequestParams(params, secureParams)
    const responseFormat = format || this.format || undefined

    if (
      type === ContentType.FormData &&
      body &&
      body !== null &&
      typeof body === "object"
    ) {
      body = this.createFormData(body as Record<string, unknown>)
    }

    if (
      type === ContentType.Text &&
      body &&
      body !== null &&
      typeof body !== "string"
    ) {
      body = JSON.stringify(body)
    }

    return this.instance
      .request({
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type && type !== ContentType.FormData
            ? { "Content-Type": type }
            : {}),
        },
        params: query,
        responseType: responseFormat,
        data: body,
        url: path,
      })
      .then((response) => response.data)
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
  booking = {
    /**
     * No description
     *
     * @tags Booking
     * @name SendBookingRequest
     * @summary Send booking request. Limit 1024 znaki na komentarz jak coś B-)
     * @request POST:/Booking/SendBookingRequest
     * @secure
     * @response `200` `EmptyResponse` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    sendBookingRequest: (
      data: SendBookingRequestDto,
      params: RequestParams = {}
    ) =>
      this.request<EmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Booking/SendBookingRequest`,
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
     * @tags Booking
     * @name ListMyBookingRequests
     * @summary List my booking requests
     * @request POST:/Booking/ListMyBookingRequests
     * @secure
     * @response `200` `PagedDataBookingRequestDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    listMyBookingRequests: (
      data: ListMyBookingRequestsDto,
      params: RequestParams = {}
    ) =>
      this.request<PagedDataBookingRequestDto, ErrorResponse | EmptyResponse>({
        path: `/Booking/ListMyBookingRequests`,
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
     * @tags Booking
     * @name ListIncomingBookingRequests
     * @summary List incoming booking requests
     * @request POST:/Booking/ListIncomingBookingRequests
     * @secure
     * @response `200` `PagedDataBookingRequestDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    listIncomingBookingRequests: (
      data: ListIncomingBookingRequestsDto,
      params: RequestParams = {}
    ) =>
      this.request<PagedDataBookingRequestDto, ErrorResponse | EmptyResponse>({
        path: `/Booking/ListIncomingBookingRequests`,
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
     * @tags Booking
     * @name RespondToBookingRequest
     * @summary Respond to booking request
     * @request POST:/Booking/RespondToBookingRequest
     * @secure
     * @response `200` `PagedDataBookingRequestDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    respondToBookingRequest: (
      data: RespondToBookingRequestDto,
      params: RequestParams = {}
    ) =>
      this.request<PagedDataBookingRequestDto, ErrorResponse | EmptyResponse>({
        path: `/Booking/RespondToBookingRequest`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
  comment = {
    /**
     * No description
     *
     * @tags Comment
     * @name SubmitComment
     * @summary Submit comments
     * @request POST:/Comment/SubmitComment
     * @secure
     * @response `200` `OkResponseSubmittedCommentDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    submitComment: (data: SubmitCommentDto, params: RequestParams = {}) =>
      this.request<
        OkResponseSubmittedCommentDto,
        ErrorResponse | EmptyResponse
      >({
        path: `/Comment/SubmitComment`,
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
     * @tags Comment
     * @name LikeComment
     * @summary Like comment
     * @request POST:/Comment/LikeComment
     * @secure
     * @response `200` `OkResponseEmptyResponse` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    likeComment: (data: LikeCommentDto, params: RequestParams = {}) =>
      this.request<OkResponseEmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Comment/LikeComment`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),
  }
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
      params: RequestParams = {}
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
      params: RequestParams = {}
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
    deleteForegroundPhoto: (
      data: DeleteForegroundPhotoDto,
      params: RequestParams = {}
    ) =>
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
    deleteBackgroundPhoto: (
      data: DeleteBackgroundPhotoDto,
      params: RequestParams = {}
    ) =>
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
     * @response `200` `OkResponsePagedDataBriefUserDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    getTopArtists: (data: GetTopArtistsDto, params: RequestParams = {}) =>
      this.request<
        OkResponsePagedDataBriefUserDto,
        ErrorResponse | EmptyResponse
      >({
        path: `/Identity/GetTopArtists`,
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
     * @name SearchUsers
     * @summary Search users
     * @request POST:/Identity/SearchUsers
     * @secure
     * @response `200` `OkResponsePagedDataBriefUserDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    searchUsers: (data: SearchUsersDto, params: RequestParams = {}) =>
      this.request<
        OkResponsePagedDataBriefUserDto,
        ErrorResponse | EmptyResponse
      >({
        path: `/Identity/SearchUsers`,
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
      this.request<
        OkResponsePagedDataCategoryDto,
        ErrorResponse | EmptyResponse
      >({
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
      params: RequestParams = {}
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

    /**
     * No description
     *
     * @tags Post
     * @name SearchPosts
     * @summary Search posts
     * @request POST:/Post/SearchPosts
     * @secure
     * @response `200` `OkResponsePagedDataBriefPostDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    searchPosts: (data: SearchPostsDto, params: RequestParams = {}) =>
      this.request<
        OkResponsePagedDataBriefPostDto,
        ErrorResponse | EmptyResponse
      >({
        path: `/Post/SearchPosts`,
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
     * @tags Post
     * @name LikePost
     * @summary Like post
     * @request POST:/Post/LikePost
     * @secure
     * @response `200` `OkResponseEmptyResponse` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    likePost: (data: LikePostDto, params: RequestParams = {}) =>
      this.request<OkResponseEmptyResponse, ErrorResponse | EmptyResponse>({
        path: `/Post/LikePost`,
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
     * @tags Post
     * @name GetUserPosts
     * @summary Get user posts
     * @request POST:/Post/GetUserPosts
     * @secure
     * @response `200` `OkResponsePagedDataBriefPostDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    getUserPosts: (data: GetUserPostsDto, params: RequestParams = {}) =>
      this.request<
        OkResponsePagedDataBriefPostDto,
        ErrorResponse | EmptyResponse
      >({
        path: `/Post/GetUserPosts`,
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
     * @tags Post
     * @name GetPostFeed
     * @summary Get post feed
     * @request POST:/Post/GetPostFeed
     * @secure
     * @response `200` `OkResponsePagedDataBriefPostDto` Success
     * @response `400` `ErrorResponse` Bad Request
     * @response `401` `EmptyResponse` Unauthorized
     * @response `403` `EmptyResponse` Forbidden
     * @response `500` `ErrorResponse` Server Error
     */
    getPostFeed: (data: GetPostFeedDto, params: RequestParams = {}) =>
      this.request<
        OkResponsePagedDataBriefPostDto,
        ErrorResponse | EmptyResponse
      >({
        path: `/Post/GetPostFeed`,
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
