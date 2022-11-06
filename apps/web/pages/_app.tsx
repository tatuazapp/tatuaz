import { Auth0Provider } from "@auth0/auth0-react"
import { ChakraProvider } from "@chakra-ui/react"
import { PrismicPreview } from "@prismicio/next"
import { PrismicProvider } from "@prismicio/react"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import { AppProps } from "next/app"
import Head from "next/head"
import Link from "next/link"
import { IntlProvider } from "react-intl"
import { ThemeProvider } from "styled-components"
import { useApiInit } from "../api/apiClient"
import { currentLocale, messages } from "../i18n"
import { repositoryName } from "../prismicio"
import chakraTheme from "../styles/chakra"
import "../styles/global.css"
import { theme } from "../styles/theme"

const queryClient = new QueryClient()

function App({ Component, pageProps }: AppProps) {
  useApiInit()

  return (
    <QueryClientProvider client={queryClient}>
      <IntlProvider locale={currentLocale} messages={messages[currentLocale]}>
        <ThemeProvider theme={theme}>
          <ChakraProvider theme={chakraTheme}>
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
                clientId={process.env.NEXT_PUBLIC_AUTH0_CLIENT_ID}
                domain={process.env.NEXT_PUBLIC_AUTH0_DOMAIN}
                redirectUri={process.env.NEXT_PUBLIC_AUTH0_REDIRECT_URI}
              >
                <Head>
                  <title>Tatuaż App</title>
                </Head>
                <PrismicPreview repositoryName={repositoryName}>
                  <main className="app">
                    <Component {...pageProps} />
                  </main>
                </PrismicPreview>
              </Auth0Provider>
            </PrismicProvider>
          </ChakraProvider>
        </ThemeProvider>
      </IntlProvider>
    </QueryClientProvider>
  )
}

export default App
