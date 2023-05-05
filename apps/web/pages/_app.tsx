/* eslint-disable @typescript-eslint/no-non-null-assertion */
import { Auth0Provider } from "@auth0/auth0-react"
import { ChakraProvider } from "@chakra-ui/react"
import { PrismicPreview } from "@prismicio/next"
import { PrismicProvider } from "@prismicio/react"
import vh from "@sect/100vh"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { AppProps } from "next/app"
import Head from "next/head"
import Link from "next/link"
import { useEffect } from "react"
import { IntlProvider } from "react-intl"
import { ThemeProvider } from "styled-components"
import ApiInit from "../components/auth/ApiInit"
import { ModalProvider } from "../components/common/modals/useModals"
import { currentLocale, messages } from "../i18n"
import { repositoryName } from "../prismicio"
import chakraTheme from "../styles/chakra"
import "../styles/global.css"
import "slick-carousel/slick/slick.css"
import "slick-carousel/slick/slick-theme.css"
import { theme, themeWithBreakpoints } from "../styles/theme"

export const queryClient = new QueryClient()

const themeMerged = {
  ...theme,
  ...themeWithBreakpoints,
}

function App({ Component, pageProps }: AppProps) {
  useEffect(() => {
    vh.init()
  }, [])

  return (
    <QueryClientProvider client={queryClient}>
      <IntlProvider locale={currentLocale} messages={messages[currentLocale]}>
        <ThemeProvider theme={themeMerged}>
          <ChakraProvider theme={chakraTheme}>
            <ModalProvider>
              <PrismicProvider
                internalLinkComponent={({ href, ...props }) => (
                  <Link href={href}>
                    <a {...props} />
                  </Link>
                )}
              >
                <Auth0Provider
                  audience={process.env.NEXT_PUBLIC_AUTH0_AUDIENCE}
                  cacheLocation="localstorage"
                  clientId={process.env.NEXT_PUBLIC_AUTH0_CLIENT_ID!}
                  domain={process.env.NEXT_PUBLIC_AUTH0_DOMAIN!}
                  redirectUri={process.env.NEXT_PUBLIC_AUTH0_REDIRECT_URI}
                >
                  <Head>
                    <title>Tatuaż App</title>
                  </Head>
                  <ApiInit />
                  <PrismicPreview repositoryName={repositoryName}>
                    <main className="app">
                      <Component {...pageProps} />
                    </main>
                  </PrismicPreview>
                </Auth0Provider>
              </PrismicProvider>
            </ModalProvider>
          </ChakraProvider>
        </ThemeProvider>
      </IntlProvider>
    </QueryClientProvider>
  )
}

export default App
