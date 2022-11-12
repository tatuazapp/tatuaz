import { FunctionComponent } from "react"
import useApiInit from "../../../api/hooks/useApiInit"
import useMe from "../../../api/hooks/useMe"

const ApiInit: FunctionComponent = () => {
  // Initialize access token to be available in all requests
  useApiInit()

  // Make sure use has his data saved. useMe hook sends user data to /SignUp endpoint if user is not in database
  useMe()

  return null
}

export default ApiInit
