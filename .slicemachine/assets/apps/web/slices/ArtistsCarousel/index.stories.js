import MyComponent from "../../../../../../apps/web/slices/ArtistsCarousel"

export default {
  title: "apps/web/slices/ArtistsCarousel",
}

export const _Default = () => (
  <MyComponent
    slice={{
      variation: "default",
      version: "sktwi1xtmkfgx8626",
      items: [
        {
          title: [
            {
              type: "paragraph",
              text: "Amet culpa ea quis consectetur excepteur reprehenderit sit laborum non dolor esse. Velit ea et minim culpa magna sint laboris incididunt ut consequat.",
              spans: [],
            },
          ],
          description: [
            {
              type: "paragraph",
              text: "Laboris adipisicing ex aliqua nulla id laborum consequat. Elit ea laborum consequat ullamco anim ullamco cillum aute amet culpa excepteur occaecat aute.",
              spans: [],
            },
          ],
          photo: {
            dimensions: { width: 900, height: 500 },
            alt: null,
            copyright: null,
            url: "https://images.unsplash.com/photo-1515378960530-7c0da6231fb1",
          },
        },
      ],
      primary: {},
      slice_type: "artists_carousel",
      id: "_Default",
    }}
  />
)
_Default.storyName = ""
