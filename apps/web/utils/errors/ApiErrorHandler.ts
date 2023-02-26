import { ErrorResponse } from "../../api/tatuazApi"

type ErrorCodeBase = string

class ApiErrorHandler<T extends ErrorCodeBase> {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private handlers: Record<T, () => void> = {} as any
  private defaultHandler?: () => void

  constructor(private error: ErrorResponse) {}

  handle(code: T | "*", handler: () => void): this {
    if (code === "*") {
      this.defaultHandler = handler
    } else {
      this.handlers[code] = handler
    }
    return this
  }

  run(): void {
    this.error.errors?.forEach((error) => {
      const handler = this.handlers[error.code as T] || this.defaultHandler
      if (handler) {
        handler()
      }
    })
  }
}

export default ApiErrorHandler
