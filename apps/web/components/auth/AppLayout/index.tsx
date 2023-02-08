import { FunctionComponent } from "react"
import Footer from "./Footer"
import Header from "./Header"

type AppLayoutProps = {
  children?: React.ReactNode
}

const AppLayout: FunctionComponent<AppLayoutProps> = ({ children }) => (
  <>
    <Header />
    {children}
    <Footer />
  </>
)
export default AppLayout
