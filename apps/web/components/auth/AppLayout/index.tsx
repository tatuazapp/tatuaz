import { FunctionComponent } from "react"
import Header from "./Header"

type AppLayoutProps = {
  children?: React.ReactNode
}

const AppLayout: FunctionComponent<AppLayoutProps> = ({ children }) => (
  <>
    <Header />
    {children}
  </>
)
export default AppLayout
