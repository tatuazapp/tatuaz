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
      <Head>
        <title>Oliwka Brazil</title>
      </Head>
      <PrismicPreview repositoryName={repositoryName}>
        <main className="app">
          <Component {...pageProps} />
        </main>
      </PrismicPreview>
    </PrismicProvider>
  )
}

export default CustomApp
