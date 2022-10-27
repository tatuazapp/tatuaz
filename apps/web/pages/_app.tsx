import { Auth0Provider } from "@auth0/auth0-react"
import { PrismicPreview } from "@prismicio/next"
import { PrismicProvider } from "@prismicio/react"
import { AppProps } from "next/app"
import Head from "next/head"
import Link from "next/link"
import { repositoryName } from "../prismicio"
import "./styles.css"

function CustomApp({ Component, pageProps }: AppProps) {
  return (
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
          <title>Oliwka Brazil</title>
        </Head>
        <PrismicPreview repositoryName={repositoryName}>
          <main className="app">
            <Component {...pageProps} />
          </main>
        </PrismicPreview>
      </Auth0Provider>
    </PrismicProvider>
  )
}

export default CustomApp
